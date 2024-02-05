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

Example SVGs
===
The following SVGs are generated using the SimplifyShapesTests.cs


Square (~10000 points)
---
![high-resolution-square.svg](high-resolution-square.svg) 


Simplified square (5 points)
---
![high-resolution-square-simplified.svg](high-resolution-square-simplified.svg)


Cicle (~6284 points)
---
![high-resolution-circle.svg](high-resolution-circle.svg) 


Simplified circle (~33 points)
---
![high-resolution-circle-simplified.svg](high-resolution-circle-simplified.svg) 


Very simplified circle (~9 points)
---
![high-resolution-circle-ultra-simplified.svg](high-resolution-circle-ultra-simplified.svg) 



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


Original description from simplify-net
===========

A .NET port of simplify-js (https://github.com/mourner/simplify-js)
Polyline simplification library by Vladimir Agafonkin, extracted from Leaflet.
For license see the original project or https://github.com/mourner/simplify-js/blob/master/LICENSE 
