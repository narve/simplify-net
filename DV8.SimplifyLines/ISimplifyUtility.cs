using System;
using System.Collections.Generic;
using System.Numerics;

namespace DV8.SimplifyLines;

public interface ISimplifyUtility
{
    /// <summary>
    /// Simplifies a list of points to a shorter list of points.
    /// </summary>
    /// <param name="points">Points original list of points</param>
    /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
    /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
    /// <returns>Simplified list of points</returns>
    List<Vector3> Simplify(ReadOnlySpan<Vector3> points, float tolerance = 0.3f, bool highestQuality = false);
}