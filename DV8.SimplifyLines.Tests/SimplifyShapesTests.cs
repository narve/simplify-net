using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace DV8.SimplifyLines.Tests;

[TestFixture]
public class SimplifyShapesTests
{
    [Test]
    public void TestShrinkSquare()
    {
        var points = CreateHighResolutionSquare();
        AreEqual(4 * 1001, points.Count);

        var simplified = SimplifyUtility.SimplifyArray(points.ToArray());
        // Note: 5 points, not 4, because we need to close the square
        AreEqual(5, simplified.Count); 
    }

    [Test]
    public void TestShrinkPolygonWithAutoResolution()
    {
        var polygon = CreateHighResolutionCurve();
        AreEqual(10_000, polygon.Count);
        
        var simplified = new SimplifyUtility().Simplify(polygon.ToArray(), 0.1f);
        // Some magic number... at least less than points.length
        AreEqual(9, simplified.Count);
    }    

    /// Make a square box with multiple points along each edge, 
    /// should simplify to 4 points + origin
    private static List<Vector3> CreateHighResolutionSquare()
    {
        const float size = 10.0f;
        const float delta = 0.01f;

        var points = new List<Vector3>();

        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new (i, 0, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new (size, i, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new (size - i, size, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new (0, size - i, 0));
        
        return points;
    }
    

    /// Make a curved line with high resolution
    /// Should simplify to less points
    private static List<Vector3> CreateHighResolutionCurve()
    {
        const float size = 10.0f;
        const float delta = 0.001f;

        var points = new List<Vector3>();

        for (var i = 0.0f; i <= size; i += delta) points.Add(new (i, i * i, 0));

        return points;
    }
}