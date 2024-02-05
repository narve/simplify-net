using System;
using System.Diagnostics;
using System.Numerics;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace DV8.SimplifyLines.Tests;

[TestFixture]
public class SimplifyUtilityTests
{
    #region SimplifyTimings
    
    [Test]
    [Ignore("Uncomment to run timings")]
    public void Simplify2DTimings()
    {
        ISimplifyUtility utility = new SimplifyUtility();
        var points = LongLine.GetPoints();
            
        var watch = Stopwatch.StartNew();
        utility.Simplify(points);
        watch.Stop();

        Console.WriteLine("First time utility warmup took: " + watch.ElapsedTicks + " ticks");

        for (var i = 0; i < 10; i++)
        {
            const int times = 1000;
            long totalTime = 0;

            for (var b = 0; b < times; b++)
            {
                watch = Stopwatch.StartNew();
                utility.Simplify(points);
                watch.Stop();
                totalTime += watch.ElapsedTicks;
            }

            Console.WriteLine(times + "x average time: " + totalTime/times + " ticks");
        }
    }

    #endregion

    [Test]
    public void SimplifyWithMultiplePointsShouldSimplifyCorrectly()
    {
        ISimplifyUtility utility = new SimplifyUtility();
        var points = new Vector3[] {
            new(224.55f,250.15f, 0),new(226.91f,244.19f, 0),new(233.31f,241.45f, 0),new(234.98f,236.06f, 0),
            new(244.21f,232.76f, 0),new(262.59f,215.31f, 0),new(267.76f,213.81f, 0),new(273.57f,201.84f, 0),
            new(273.12f,192.16f, 0),new(277.62f,189.03f, 0),new(280.36f,181.41f, 0),new(286.51f,177.74f, 0),
            new(292.41f,159.37f, 0),new(296.91f,155.64f, 0),new(314.95f,151.37f, 0),new(319.75f,145.16f, 0),
            new(330.33f,137.57f, 0),new(341.48f,139.96f, 0),new(369.98f,137.89f, 0),new(387.39f,142.51f, 0),
            new(391.28f,139.39f, 0),new(409.52f,141.14f, 0),new(414.82f,139.75f, 0),new(427.72f,127.30f, 0),
            new(439.60f,119.74f, 0),new(474.93f,107.87f, 0),new(486.51f,106.75f, 0),new(489.20f,109.45f, 0),
            new(493.79f,108.63f, 0),new(504.74f,119.66f, 0),new(512.96f,122.35f, 0),new(518.63f,120.89f, 0),
            new(524.09f,126.88f, 0),new(529.57f,127.86f, 0),new(534.21f,140.93f, 0),new(539.27f,147.24f, 0),
            new(567.69f,148.91f, 0),new(575.25f,157.26f, 0),new(580.62f,158.15f, 0),new(601.53f,156.85f, 0),
            new(617.74f,159.86f, 0),new(622.00f,167.04f, 0),new(629.55f,194.60f, 0),new(638.90f,195.61f, 0),
            new(641.26f,200.81f, 0),new(651.77f,204.56f, 0),new(671.55f,222.55f, 0),new(683.68f,217.45f, 0),
            new(695.25f,219.15f, 0),new(700.64f,217.98f, 0),new(703.12f,214.36f, 0),new(712.26f,215.87f, 0),
            new(721.49f,212.81f, 0),new(727.81f,213.36f, 0),new(729.98f,208.73f, 0),new(735.32f,208.20f, 0),
            new(739.94f,204.77f, 0),new(769.98f,208.42f, 0),new(779.60f,216.87f, 0),new(784.20f,218.16f, 0),
            new(800.24f,214.62f, 0),new(810.53f,219.73f, 0),new(817.19f,226.82f, 0),new(820.77f,236.17f, 0),
            new(827.23f,236.16f, 0),new(829.89f,239.89f, 0),new(851.00f,248.94f, 0),new(859.88f,255.49f, 0),
            new(865.21f,268.53f, 0),new(857.95f,280.30f, 0),new(865.48f,291.45f, 0),new(866.81f,298.66f, 0),
            new(864.68f,302.71f, 0),new(867.79f,306.17f, 0),new(859.87f,311.37f, 0),new(860.08f,314.35f, 0),
            new(858.29f,314.94f, 0),new(858.10f,327.60f, 0),new(854.54f,335.40f, 0),new(860.92f,343.00f, 0),
            new(856.43f,350.15f, 0),new(851.42f,352.96f, 0),new(849.84f,359.59f, 0),new(854.56f,365.53f, 0),
            new(849.74f,370.38f, 0),new(844.09f,371.89f, 0),new(844.75f,380.44f, 0),new(841.52f,383.67f, 0),
            new(839.57f,390.40f, 0),new(845.59f,399.05f, 0),new(848.40f,407.55f, 0),new(843.71f,411.30f, 0),
            new(844.09f,419.88f, 0),new(839.51f,432.76f, 0),new(841.33f,441.04f, 0),new(847.62f,449.22f, 0),
            new(847.16f,458.44f, 0),new(851.38f,462.79f, 0),new(853.97f,471.15f, 0),new(866.36f,480.77f, 0)
        };

        var simplified = new Vector3[] {
            new(224.55f,250.15f, 0),new(267.76f,213.81f, 0),new(296.91f,155.64f, 0),new(330.33f,137.57f, 0),
            new(409.52f,141.14f, 0),new(439.60f,119.74f, 0),new(486.51f,106.75f, 0),new(529.57f,127.86f, 0),
            new(539.27f,147.24f, 0),new(617.74f,159.86f, 0),new(629.55f,194.60f, 0),new(671.55f,222.55f, 0),
            new(727.81f,213.36f, 0),new(739.94f,204.77f, 0),new(769.98f,208.42f, 0),new(779.60f,216.87f, 0),
            new(800.24f,214.62f, 0),new(820.77f,236.17f, 0),new(859.88f,255.49f, 0),new(865.21f,268.53f, 0),
            new(857.95f,280.30f, 0),new(867.79f,306.17f, 0),new(859.87f,311.37f, 0),new(854.54f,335.40f, 0),
            new(860.92f,343.00f, 0),new(849.84f,359.59f, 0),new(854.56f,365.53f, 0),new(844.09f,371.89f, 0),
            new(839.57f,390.40f, 0),new(848.40f,407.55f, 0),new(839.51f,432.76f, 0),new(853.97f,471.15f, 0),
            new(866.36f,480.77f, 0)
        };
        
        var result = utility.Simplify(points, 5);

        AreEqual(simplified.Length, result.Count);
        That(simplified, Is.EquivalentTo(result));
    }

    [Test]
    public void SimplifySinglePointResultShouldOnlyContainSinglePoint()
    {
        ISimplifyUtility utility = new SimplifyUtility();
        var point = new Vector3(224.55f, 250.15f, 0);
        var result = utility.Simplify(new[] { point });

        AreEqual(1, result.Count);
        AreEqual(result[0].X, point.X);
        AreEqual(result[0].Y, point.Y);
    }


    [Test]
    public void SimplifyWithEmptyArrayShouldShouldReturnEmptyList() => 
        IsEmpty(((ISimplifyUtility)new SimplifyUtility()).Simplify(Array.Empty<Vector3>()));
}