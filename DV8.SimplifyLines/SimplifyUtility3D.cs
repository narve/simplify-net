// High-performance polyline simplification library
//
// This is a port of simplify-js by Vladimir Agafonkin, Copyright (c) 2012
// https://github.com/mourner/simplify-js
// 
// The code is ported from JavaScript to C#.
// The library is created as portable and 
// is targeting multiple Microsoft plattforms.
//
// This library was ported by imshz @ http://www.shz.no
// https://github.com/imshz/simplify-net
//
// This code is provided as is by the author. For complete license please
// read the original license at https://github.com/mourner/simplify-js

using System.Collections.Generic;
using System.Numerics;

namespace DV8.SimplifyLines
{
    /// <summary>
    /// Simplification of a 3D-polyline. 
    /// Use only the 3D version if your point contains altitude information, if no altitude information is provided the 2D library gives a 20% performance gain.
    /// </summary>
    public class SimplifyUtility3D : ISimplifyUtility
    {
        // square distance between 2 points
        private double GetSquareDistance(Vector3 p1, Vector3 p2)
        {
            double dx = p1.X - p2.X,
                dy = p1.Y - p2.Y,
                dz = p1.Z - p2.Z;

            return dx*dx + dy*dy + dz*dz;
        }

        // square distance from a point to a segment
        private double GetSquareSegmentDistance(Vector3 p, Vector3 p1, Vector3 p2)
        {
            var x = p1.X;
            var y = p1.Y;
            var z = p1.Z;
            var dx = p2.X - x;
            var dy = p2.Y - y;
            var dz = p2.Z - z;

            if (!dx.Equals(0.0) || !dy.Equals(0.0) || !dz.Equals(0.0))
            {
                var t = ((p.X - x) * dx + (p.Y - y) * dy + (p.Z - z) * dz) / (dx * dx + dy * dy + dz * dz);

                if (t > 1)
                {
                    x = p2.X;
                    y = p2.Y;
                    z = p2.Z;
                }
                else if (t > 0)
                {
                    x += dx*t;
                    y += dy*t;
                    z += dz*t;
                }
            }

            dx = p.X - x;
            dy = p.Y - y;
            dz = p.Z - z;

            return dx*dx + dy*dy + dz * dz;
        }

        // rest of the code doesn't care about point format

        // basic distance-based simplification
        private List<Vector3> SimplifyRadialDistance(Vector3[] points, float sqTolerance)
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

            if (!prevPoint.Equals(point))
                newPoints.Add(point);

            return newPoints;
        }

        // simplification using optimized Douglas-Peucker algorithm with recursion elimination
        private List<Vector3> SimplifyDouglasPeucker(Vector3[] points, double sqTolerance)
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

                for (int? i = first + 1; i < last; i++)
                {
                    var sqDist = GetSquareSegmentDistance(points[i.Value], points[first.Value], points[last.Value]);

                    if (sqDist > maxSqDist)
                    {
                        index = i;
                        maxSqDist = sqDist;
                    }
                }

                if (maxSqDist > sqTolerance)
                {
                    markers[index.Value] = 1;
                    stack.AddRange(new[] { first, index, index, last });
                }


                if (stack.Count > 0)
                {
                    last = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);
                }
                else
                    last = null;

                if (stack.Count > 0)
                {
                    first = stack[stack.Count - 1];
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
        public List<Vector3> Simplify(Vector3[] points, float tolerance = 0.3f, bool highestQuality = false)
        {
            if(points == null || points.Length == 0)
                return new List<Vector3>();

            var sqTolerance = tolerance*tolerance;

            if (!highestQuality)
            {
                List<Vector3> points2 = SimplifyRadialDistance(points, sqTolerance);
                return SimplifyDouglasPeucker(points2.ToArray(), sqTolerance);
            }

            return SimplifyDouglasPeucker(points, sqTolerance);
        }

        /// <summary>
        /// Simplifies a list of points to a shorter list of points.
        /// </summary>
        /// <param name="points">Points original list of points</param>
        /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
        /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
        /// <returns>Simplified list of points</returns>
        public static List<Vector3> SimplifyArray(Vector3[] points, float tolerance = 0.3f, bool highestQuality = false)
        {
            return new SimplifyUtility().Simplify(points, tolerance, highestQuality);
        }
    }
}
