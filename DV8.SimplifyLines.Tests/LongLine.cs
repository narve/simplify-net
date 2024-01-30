using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace DV8.SimplifyLines.Tests;

public static class LongLine
{
    public static Vector3[] GetPoints()
    {
        var assembly = Assembly.GetAssembly(typeof(SimplifyUtilityTests))!;
        var dataFile = assembly
            .GetManifestResourceNames()
            .SingleOrDefault(n => n.Contains("long-line"));
        ArgumentNullException.ThrowIfNull(dataFile);
        using var stream = assembly.GetManifestResourceStream(dataFile);
        ArgumentNullException.ThrowIfNull(stream);
        using var reader = new StreamReader(stream);
        var l = new List<Vector3>();
        while (reader.ReadLine() is { } s)
        {
            var parts = s.Split(",");
            var p1 = float.Parse(parts[0]); 
            var p2 = float.Parse(parts[1]); 
                
            l.Add(new Vector3(p1,p2, 0));
        }

        return l.ToArray();
    }
}