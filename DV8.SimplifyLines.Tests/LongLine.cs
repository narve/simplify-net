using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DV8.SimplifyLines.Tests;

public class LongLine
{
    public static Point[] GetPoints()
    {
        var assembly = Assembly.GetAssembly(typeof(SimplifyUtilityTests))!;
        var dataFile = assembly
            .GetManifestResourceNames()
            .SingleOrDefault(n => n.Contains("long-line"));
        ArgumentNullException.ThrowIfNull(dataFile);
        using var stream = assembly.GetManifestResourceStream(dataFile);
        ArgumentNullException.ThrowIfNull(stream);
        using var reader = new StreamReader(stream);
        var l = new List<Point>();
        while (reader.ReadLine() is { } s)
        {
            var parts = s.Split(",");
            var p1 = float.Parse(parts[0]); 
            var p2 = float.Parse(parts[1]); 
                
            l.Add(new Point(p1,p2));
        }

        return l.ToArray();
    }
        
}