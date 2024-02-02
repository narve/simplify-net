using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        AreEqual(4 * 1001 +1, points.Count);
        SaveAsSvg("high-resolution-square", points);


        var simplified = SimplifyUtility.SimplifyArray(points.ToArray());
        SaveAsSvg("high-resolution-square-simplified", points);
        // Note: 5 points, not 4, because we need to close the square
        AreEqual(5, simplified.Count);
    }

    [Test]
    public void TestShrinkHighResolutionCircle()
    {
        var points = CreateHighResolutionCircle();
        SaveAsSvg("high-resolution-circle", points);
        // Approx 2*PI*1000
        AreEqual(6284, points.Count);

        var simplified = new SimplifyUtility().Simplify(points.ToArray(), 0.1f);
        SaveAsSvg("high-resolution-circle-simplified", points);
        // Some magic number... at least less than points.length
        AreEqual(33, simplified.Count);
    }

    /// Make a square box with multiple points along each edge, 
    /// should simplify to 4 points + origin
    private static List<Vector3> CreateHighResolutionSquare()
    {
        const float size = 500.0f;
        const float delta = 0.5f;

        var points = new List<Vector3>();

        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new(i, 0, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new(size, i, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new(size - i, size, 0));
        for (var i = 0.0f; i <= size; i += delta)
            points.Add(new(0, size - i, 0));
        
        // Close the square: 
        points.Add(new(0.0f,0.0f,0.0f));

        return points;
    }


    /// Make a curved line with high resolution
    /// Should simplify to less points
    private static List<Vector3> CreateHighResolutionCircle()
    {
        const float radius = 20.0f;
        const float delta = 0.001f;

        var points = new List<Vector3>();
        
        double theta = 0;
        while (theta <= 2 * Math.PI)
        {
            // convert polar to cartesian coordinates
            float x = (float)(radius * Math.Cos(theta));
            float y = (float)(radius * Math.Sin(theta));

            // add point to list
            points.Add(new Vector3(x+radius*2, y+radius*2, 0));

            // increment angle by delta
            theta += delta;
        }
        //
        // for (var i = 0.0f; i <= radius; i += delta)
        //     points.Add(new(i, (radius-i)*(radius-i), 0));
        // for (var i = radius; i >= 0; i -= delta)
        //     points.Add(new(i, -i*i, 0));
        // for (var i = 0.0f; i <= radius; i += delta)
        //     points.Add(new(-i, i*i, 0));

        return points;
    }
    

    private void SaveAsSvg(string fileName, List<Vector3> points)
    {
        var start = points.First();
        var pointString = $"M {start.X} {start.Y} " + string.Join(
            " ",
            points.Select(p => $"L {p.X} {p.Y}").ToArray());

        // pointString = "M 0 0 L 90 0 L 90 90 L 0 90 L 0 0";

        var svg = @$"<?xml version=""1.0"" encoding=""iso-8859-1""?>
<!DOCTYPE svg PUBLIC ""-//W3C//DTD SVG 1.1//EN"" ""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">
<svg
        version=""1.1""
        xmlns=""http://www.w3.org/2000/svg""
>
<g>
    <path d=""{pointString}""/>
</g>
</svg>";
        var d = new DirectoryInfo(".");
        while(!d.Name.Contains("DV8.SimplifyLines.Tests"))
            d = d.Parent;
        d = d.Parent;
        
        // var f = new FileInfo(d, fileName + ".svg");
        var abs = Path.Combine(d.FullName, fileName + ".svg");
        File.WriteAllText(abs, svg);
    }
    
}