﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using NUnit.Framework;

namespace DV8.SimplifyLines.Tests;

[TestFixture]
public class SimplifyUtilityTests
{
    #region SimplifyTimings

    // [Test]
    // public void ConvertLongLine()
    // {
    //     var points = LongLine.GetPoints(); 
    //     
    //     var file = new FileInfo("longline.txt");
    //     if (file.Exists)
    //         file.Delete();
    //     Console.WriteLine("Filename: " + file.FullName);
    //     var writer = new StreamWriter(file.OpenWrite());
    //     foreach (var point in points)
    //     {
    //         writer.WriteLine(point.X + ", " + point.Y);
    //     }
    //     writer.Close();
    //     // points.
    // }

    [Test]
    [Ignore("Uncomment to run timings")]
    public void Simplify2DTimings() => SimplifyTimings(new SimplifyUtility());

    [Test]
    [Ignore("Uncomment to run timings")]
    public void Simplify3DTimings() => SimplifyTimings(new SimplifyUtility3D());

    private static void SimplifyTimings(ISimplifyUtility utility)
    {
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

    #region SimplifyWithMultiplePointsShouldSimplfyCorrectly

    [Test]
    public void SimplifyWithMultiplePointsShouldSimplifyCorrectly()
    {
        SimplifyWithMultiplePointsShouldSimplifyCorrectly(new SimplifyUtility());
    }

    [Test]
    public void Simplify3DWithMultiplePointsShouldSimplifyCorrectly()
    {
        SimplifyWithMultiplePointsShouldSimplifyCorrectly(new SimplifyUtility3D());
    }

    private static void SimplifyWithMultiplePointsShouldSimplifyCorrectly(ISimplifyUtility utility)
    {
        var points = new[] {
            new Point(224.55,250.15),new Point(226.91,244.19),new Point(233.31,241.45),new Point(234.98,236.06),
            new Point(244.21,232.76),new Point(262.59,215.31),new Point(267.76,213.81),new Point(273.57,201.84),
            new Point(273.12,192.16),new Point(277.62,189.03),new Point(280.36,181.41),new Point(286.51,177.74),
            new Point(292.41,159.37),new Point(296.91,155.64),new Point(314.95,151.37),new Point(319.75,145.16),
            new Point(330.33,137.57),new Point(341.48,139.96),new Point(369.98,137.89),new Point(387.39,142.51),
            new Point(391.28,139.39),new Point(409.52,141.14),new Point(414.82,139.75),new Point(427.72,127.30),
            new Point(439.60,119.74),new Point(474.93,107.87),new Point(486.51,106.75),new Point(489.20,109.45),
            new Point(493.79,108.63),new Point(504.74,119.66),new Point(512.96,122.35),new Point(518.63,120.89),
            new Point(524.09,126.88),new Point(529.57,127.86),new Point(534.21,140.93),new Point(539.27,147.24),
            new Point(567.69,148.91),new Point(575.25,157.26),new Point(580.62,158.15),new Point(601.53,156.85),
            new Point(617.74,159.86),new Point(622.00,167.04),new Point(629.55,194.60),new Point(638.90,195.61),
            new Point(641.26,200.81),new Point(651.77,204.56),new Point(671.55,222.55),new Point(683.68,217.45),
            new Point(695.25,219.15),new Point(700.64,217.98),new Point(703.12,214.36),new Point(712.26,215.87),
            new Point(721.49,212.81),new Point(727.81,213.36),new Point(729.98,208.73),new Point(735.32,208.20),
            new Point(739.94,204.77),new Point(769.98,208.42),new Point(779.60,216.87),new Point(784.20,218.16),
            new Point(800.24,214.62),new Point(810.53,219.73),new Point(817.19,226.82),new Point(820.77,236.17),
            new Point(827.23,236.16),new Point(829.89,239.89),new Point(851.00,248.94),new Point(859.88,255.49),
            new Point(865.21,268.53),new Point(857.95,280.30),new Point(865.48,291.45),new Point(866.81,298.66),
            new Point(864.68,302.71),new Point(867.79,306.17),new Point(859.87,311.37),new Point(860.08,314.35),
            new Point(858.29,314.94),new Point(858.10,327.60),new Point(854.54,335.40),new Point(860.92,343.00),
            new Point(856.43,350.15),new Point(851.42,352.96),new Point(849.84,359.59),new Point(854.56,365.53),
            new Point(849.74,370.38),new Point(844.09,371.89),new Point(844.75,380.44),new Point(841.52,383.67),
            new Point(839.57,390.40),new Point(845.59,399.05),new Point(848.40,407.55),new Point(843.71,411.30),
            new Point(844.09,419.88),new Point(839.51,432.76),new Point(841.33,441.04),new Point(847.62,449.22),
            new Point(847.16,458.44),new Point(851.38,462.79),new Point(853.97,471.15),new Point(866.36,480.77)
        };

        var simplified = new[] {
            new Point(224.55,250.15),new Point(267.76,213.81),new Point(296.91,155.64),new Point(330.33,137.57),
            new Point(409.52,141.14),new Point(439.60,119.74),new Point(486.51,106.75),new Point(529.57,127.86),
            new Point(539.27,147.24),new Point(617.74,159.86),new Point(629.55,194.60),new Point(671.55,222.55),
            new Point(727.81,213.36),new Point(739.94,204.77),new Point(769.98,208.42),new Point(779.60,216.87),
            new Point(800.24,214.62),new Point(820.77,236.17),new Point(859.88,255.49),new Point(865.21,268.53),
            new Point(857.95,280.30),new Point(867.79,306.17),new Point(859.87,311.37),new Point(854.54,335.40),
            new Point(860.92,343.00),new Point(849.84,359.59),new Point(854.56,365.53),new Point(844.09,371.89),
            new Point(839.57,390.40),new Point(848.40,407.55),new Point(839.51,432.76),new Point(853.97,471.15),
            new Point(866.36,480.77)};

        var pointsV = points.Select(p => new Vector3((float)p.X, (float)p.Y, (float)p.Z)).ToArray();
        var simplifiedV = simplified.Select(p => new Vector3((float)p.X, (float)p.Y, (float)p.Z)).ToArray();
        
        var result = utility.Simplify(pointsV, 5);

        Assert.AreEqual(simplifiedV.Length, result.Count);
        Assert.That(simplifiedV, Is.EquivalentTo(result));
    }

    #endregion

    #region SimplifySinglePointResultSholdOnlyContainSinglePoint

    [Test]
    public void SimplifySinglePointResultShouldOnlyContainSinglePoint()
    {
        SimplifySinglePointResultShouldOnlyContainSinglePoint(new SimplifyUtility());
    }

    // [Test]
    // public void Simplify3DSinglePointResultShouldOnlyContainSinglePoint()
    // {
    //     SimplifySinglePointResultShouldOnlyContainSinglePoint(new SimplifyUtility3D());
    // }

    private static void SimplifySinglePointResultShouldOnlyContainSinglePoint(ISimplifyUtility utility)
    {
        var point = new Vector3(224.55f, 250.15f, 0);
        var result = utility.Simplify(new[] { point });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(result[0].X, point.X);
        Assert.AreEqual(result[0].Y, point.Y);
    }

    #endregion

    #region SimplifyWithEmptyArraySholdShouldReturnEmptyList

    [Test]
    public void SimplifyWithEmptyArrayShouldShouldReturnEmptyList()
    {
        SimplifyWithEmptyArrayShouldReturnEmptyList(new SimplifyUtility());
    }

    [Test]
    public void Simplify3DWithEmptyArrayShouldShouldReturnEmptyList()
    {
        SimplifyWithEmptyArrayShouldReturnEmptyList(new SimplifyUtility3D());
    }

    private static void SimplifyWithEmptyArrayShouldReturnEmptyList(ISimplifyUtility utility)
    {
        var result = utility.Simplify(Array.Empty<Vector3>());

        Assert.AreEqual(0, result.Count);
    }

    #endregion
}