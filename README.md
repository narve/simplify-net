DV8.SimplifyLines
====

An updated fork of https://github.com/imshz/simplify-net

Changes: 
- updated .Net version
- fixed spelling errors
- updated code to modern standard
- changed namespace to DV8.SimplifyLines to enable NuGet package

simplify-net
===========

A .NET port of simplify-js (https://github.com/mourner/simplify-js)<br />
Polyline simplification library by Vladimir Agafonkin, extracted from Leaflet.<br />
For license see the original project or https://github.com/mourner/simplify-js/blob/master/LICENSE <br/>
<br/>
For more info on the C# .NET ported version visit my blog @ http://shz.no

Targets
-------
This port is written in C# as a portable library.<br />
It targets: .NET Framework 4.5, Windows 8, Windows Phone 8.1 and Windows Phone Silverlight 8

Install
-------
Download the precomplied library from the bin folder. See the example for more help!

Performance
-----------
Performance for this project has absolute been in focus. All of the operations has been tuned down to maximum performance C# can provide.

Running test
------------
The tests are also ported from the original project, see own solution with the tests.<br />
I am using NUnit to setup and run the tests, nunit dll is also included.<br />
More about NUnit, go to http://nunit.org

# Example #

```C#
// Some points to test with
var points = new[] {
    new Vector3(224.55f,250.15f, 0),new Vector3(226.91f,244.19f, 0),new Vector3(233.31f,241.45f, 0),new Vector3(234.98f,236.06f, 0),
    new Vector3(244.21f,232.76f, 0),new Vector3(262.59f,215.31f, 0),new Vector3(267.76f,213.81f, 0),new Vector3(273.57f,201.84f, 0),
    new Vector3(273.12f,192.16f, 0),new Vector3(277.62f,189.03f, 0),new Vector3(280.36f,181.41f, 0),new Vector3(286.51f,177.74f, 0),
    new Vector3(292.41f,159.37f, 0),new Vector3(296.91f,155.64f, 0),new Vector3(314.95f,151.37f, 0),new Vector3(319.75f,145.16f, 0),
    new Vector3(330.33f,137.57f, 0),new Vector3(341.48f,139.96f, 0),new Vector3(369.98f,137.89f, 0),new Vector3(387.39f,142.51f, 0),
    new Vector3(391.28f,139.39f, 0),new Vector3(409.52f,141.14f, 0),new Vector3(414.82f,139.75f, 0),new Vector3(427.72f,127.30f, 0),
    new Vector3(439.60f,119.74f, 0),new Vector3(474.93f,107.87f, 0),new Vector3(486.51f,106.75f, 0),new Vector3(489.20f,109.45f, 0),
    new Vector3(493.79f,108.63f, 0),new Vector3(504.74f,119.66f, 0),new Vector3(512.96f,122.35f, 0),new Vector3(518.63f,120.89f, 0),
    new Vector3(524.09f,126.88f, 0),new Vector3(529.57f,127.86f, 0),new Vector3(534.21f,140.93f, 0),new Vector3(539.27f,147.24f, 0),
    new Vector3(567.69f,148.91f, 0),new Vector3(575.25f,157.26f, 0),new Vector3(580.62f,158.15f, 0),new Vector3(601.53f,156.85f, 0),
    new Vector3(617.74f,159.86f, 0),new Vector3(622.00f,167.04f, 0),new Vector3(629.55f,194.60f, 0),new Vector3(638.90f,195.61f, 0),
    new Vector3(641.26f,200.81f, 0),new Vector3(651.77f,204.56f, 0),new Vector3(671.55f,222.55f, 0),new Vector3(683.68f,217.45f, 0),
    new Vector3(695.25f,219.15f, 0),new Vector3(700.64f,217.98f, 0),new Vector3(703.12f,214.36f, 0),new Vector3(712.26f,215.87f, 0),
    new Vector3(721.49f,212.81f, 0),new Vector3(727.81f,213.36f, 0),new Vector3(729.98f,208.73f, 0),new Vector3(735.32f,208.20f, 0),
    new Vector3(739.94f,204.77f, 0),new Vector3(769.98f,208.42f, 0),new Vector3(779.60f,216.87f, 0),new Vector3(784.20f,218.16f, 0),
    new Vector3(800.24f,214.62f, 0),new Vector3(810.53f,219.73f, 0),new Vector3(817.19f,226.82f, 0),new Vector3(820.77f,236.17f, 0),
    new Vector3(827.23f,236.16f, 0),new Vector3(829.89f,239.89f, 0),new Vector3(851.00f,248.94f, 0),new Vector3(859.88f,255.49f, 0),
    new Vector3(865.21f,268.53f, 0),new Vector3(857.95f,280.30f, 0),new Vector3(865.48f,291.45f, 0),new Vector3(866.81f,298.66f, 0),
    new Vector3(864.68f,302.71f, 0),new Vector3(867.79f,306.17f, 0),new Vector3(859.87f,311.37f, 0),new Vector3(860.08f,314.35f, 0),
    new Vector3(858.29f,314.94f, 0),new Vector3(858.10f,327.60f, 0),new Vector3(854.54f,335.40f, 0),new Vector3(860.92f,343.00f, 0),
    new Vector3(856.43f,350.15f, 0),new Vector3(851.42f,352.96f, 0),new Vector3(849.84f,359.59f, 0),new Vector3(854.56f,365.53f, 0),
    new Vector3(849.74f,370.38f, 0),new Vector3(844.09f,371.89f, 0),new Vector3(844.75f,380.44f, 0),new Vector3(841.52f,383.67f, 0),
    new Vector3(839.57f,390.40f, 0),new Vector3(845.59f,399.05f, 0),new Vector3(848.40f,407.55f, 0),new Vector3(843.71f,411.30f, 0),
    new Vector3(844.09f,419.88f, 0),new Vector3(839.51f,432.76f, 0),new Vector3(841.33f,441.04f, 0),new Vector3(847.62f,449.22f, 0),
    new Vector3(847.16f,458.44f, 0),new Vector3(851.38f,462.79f, 0),new Vector3(853.97f,471.15f, 0),new Vector3(866.36f,480.77f, 0)
};

// After simplification these points should be returned
var simplified = new[] {
    new Vector3(224.55f,250.15f, 0),new Vector3(267.76f,213.81f, 0),new Vector3(296.91f,155.64f, 0),new Vector3(330.33f,137.57f, 0),
    new Vector3(409.52f,141.14f, 0),new Vector3(439.60f,119.74f, 0),new Vector3(486.51f,106.75f, 0),new Vector3(529.57f,127.86f, 0),
    new Vector3(539.27f,147.24f, 0),new Vector3(617.74f,159.86f, 0),new Vector3(629.55f,194.60f, 0),new Vector3(671.55f,222.55f, 0),
    new Vector3(727.81f,213.36f, 0),new Vector3(739.94f,204.77f, 0),new Vector3(769.98f,208.42f, 0),new Vector3(779.60f,216.87f, 0),
    new Vector3(800.24f,214.62f, 0),new Vector3(820.77f,236.17f, 0),new Vector3(859.88f,255.49f, 0),new Vector3(865.21f,268.53f, 0),
    new Vector3(857.95f,280.30f, 0),new Vector3(867.79f,306.17f, 0),new Vector3(859.87f,311.37f, 0),new Vector3(854.54f,335.40f, 0),
    new Vector3(860.92f,343.00f, 0),new Vector3(849.84f,359.59f, 0),new Vector3(854.56f,365.53f, 0),new Vector3(844.09f,371.89f, 0),
    new Vector3(839.57f,390.40f, 0),new Vector3(848.40f,407.55f, 0),new Vector3(839.51f,432.76f, 0),new Vector3(853.97f,471.15f, 0),
    new Vector3(866.36f,480.77f, 0)

var utility = new SimplifyUtility();

var result = utility.Simplify(points, 5, false);

Assert.AreEqual(simplified.Length, result.Count);
Assert.That(simplified, Is.EquivalentTo(result));
```
