﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace DV8.SimplifyLines;

/// <summary>
/// Simplification of a 2D-polyline.
/// </summary>
public class SimplifyUtility : ISimplifyUtility
{
    public bool IsPointValid(Vector3 p) => 
        p.X <= 90.0 && p.Y is >= -90.0f and <= 180.0f && p.X >= -180.0f;
    
    // square distance between 2 points
    private static float GetSquareDistance(Vector3 p1, Vector3 p2) =>
        Vector3.DistanceSquared(p1, p2);

    // square distance from a point to a segment
    private static float GetSquareSegmentDistance(Vector3 p, Vector3 p1, Vector3 p2)
    {
        var d = p2 - p1;
        if (d != Vector3.Zero)
        {
            var t = Vector3.Dot(p-p1, d) / d.LengthSquared();;
            switch (t)
            {
                case > 1:
                    p1 = p2;
                    break;
                case > 0:
                    p1 += d * t;
                    break;
            }
        }

        d = p - p1;
        return d.LengthSquared();
    }

    // rest of the code doesn't care about point format

    // basic distance-based simplification
    private static List<Vector3> SimplifyRadialDistance(ReadOnlySpan<Vector3> points, float sqTolerance)
    {
        var prevPoint = points[0];
        var newPoints = new List<Vector3> {prevPoint};
        var point = Vector3.Zero;

        for (var i = 1; i < points.Length; i++)
        {
            point = points[i];

            if (GetSquareDistance(point, prevPoint) > sqTolerance)
            {
                newPoints.Add(point);
                prevPoint = point;
            }
        }

        if (point != Vector3.Zero && !prevPoint.Equals(point))
            newPoints.Add(point);

        return newPoints;
    }

    // simplification using optimized Douglas-Peucker algorithm with recursion elimination
    private static List<Vector3> SimplifyDouglasPeucker(ReadOnlySpan<Vector3> points, double sqTolerance)
    {
        var len = points.Length;
        var markers = new int?[len];
        int? first = 0;
        int? last = len - 1;
        int? index = 0;
        var stack = new List<int?>();
        var newPoints = new List<Vector3>();

        markers[first.Value] = markers[last.Value] = 1;

        while (last != null)
        {
            var maxSqDist = 0.0d;

            for (var i = first + 1; i < last; i++)
            {
                var sqDist = GetSquareSegmentDistance(points[i.Value], points[first.Value], points[last.Value]);
                if (!(sqDist > maxSqDist)) continue;
                
                index = i;
                maxSqDist = sqDist;
            }

            if (maxSqDist > sqTolerance)
            {
                markers[index.Value] = 1;
                stack.AddRange(new[] { first, index, index, last });
            }


            if (stack.Count > 0)
            {
                last = stack[^1];
                stack.RemoveAt(stack.Count - 1);
            }
            else
                last = null;

            if (stack.Count > 0)
            {
                first = stack[^1];
                stack.RemoveAt(stack.Count - 1);
            }
            else
                first = null;
        }

        for (var i = 0; i < len; i++)
        {
            if (markers[i] != null)
                newPoints.Add(points[i]);
        }

        return newPoints;
    }

    /// <summary>
    /// Simplifies a list of points to a shorter list of points.
    /// </summary>
    /// <param name="points">Points original list of points</param>
    /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
    /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
    /// <returns>Simplified list of points</returns>
    public List<Vector3> Simplify(ReadOnlySpan<Vector3> points, float tolerance = 0.3f, bool highestQuality = false)
    {
        if(points == null || points.Length == 0)
            return new List<Vector3>();

        var sqTolerance = tolerance*tolerance;

        if (highestQuality)
            return SimplifyDouglasPeucker(points, sqTolerance);
            
        var points2 = SimplifyRadialDistance(points, sqTolerance);
        return SimplifyDouglasPeucker(points2.ToArray(), sqTolerance);
    }

    /// <summary>
    /// Simplifies a list of points to a shorter list of points.
    /// </summary>
    /// <param name="points">Points original list of points</param>
    /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
    /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
    /// <returns>Simplified list of points</returns>
    public static List<Vector3> SimplifyArray(ReadOnlySpan<Vector3> points, float tolerance = 0.3f, bool highestQuality = false) =>
         new SimplifyUtility().Simplify(points, tolerance, highestQuality);
}