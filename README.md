DV8.SimplifyLines
====

An updated fork/rewrite of simplify.net (https://github.com/imshz/simplify-net), 
which again is a fork/rewrite of simplify-js (https://github.com/mourner/simplify-js) 

Installation / Nuget
===

See https://www.nuget.org/packages/DV8.SimplifyLines/


Highlights
===

- Simplify lines using the Douglas-Peucker algorithm or Radial-Distance algorithm
- Simple syntax/usage, see below
- Configurable resolution
- Uses standard dotnet data structures (Vector3)
- Dependency free


Usage
===

See SimplifyShapesTests.cs for full code and more examples

```csharp
        var points = CreateHighResolutionSquare();
        AreEqual(4 * 1001, points.Count);

        var simplified = SimplifyUtility.SimplifyArray(points.ToArray());
        // Note: 5 points, not 4, because we need to close the square
        AreEqual(5, simplified.Count); 
```


Changes from simplify.net:
===

- updated .Net version (now supports dotnet 6+)
- fixed spelling errors
- updated code to modern standard
- changed namespace to DV8.SimplifyLines to enable NuGet package
- changed data structure to Vector3, to enable hardware acceleration


Developers
===
[Narve Sætre](https://github.com/narve) and [Kai Drange](https://github.com/kaidrange)

simplify-net
===========

A .NET port of simplify-js (https://github.com/mourner/simplify-js)
Polyline simplification library by Vladimir Agafonkin, extracted from Leaflet.
For license see the original project or https://github.com/mourner/simplify-js/blob/master/LICENSE 
