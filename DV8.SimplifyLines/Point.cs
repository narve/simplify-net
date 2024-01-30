﻿// High-performance polyline simplification library
//
// This is a port of simplify-js by Vladimir Agafonkin, Copyright (c) 2012
// https://github.com/mourner/simplify-js
// 
// The code is ported from JavaScript to C#.
// The library is created as portable and 
// is targeting multiple Microsoft platforms.
//
// This library was ported by imshz @ http://www.shz.no
// https://github.com/imshz/simplify-net
//
// This code is provided as is by the author. For complete license please
// read the original license at https://github.com/mourner/simplify-js

using System;

namespace DV8.SimplifyLines;

public class Point : IEquatable<Point>
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;

    public Point(double x, double y, double z = 0)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public bool IsValid => 
        X <= 90.0 && Y is >= -90.0 and <= 180.0 && X >= -180.0;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Point) && obj.GetType() != typeof(Point))
            return false;
        return Equals(obj as Point);
    }

    public bool Equals(Point other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return other.X.Equals(X) && other.Y.Equals(Y) && other.Z.Equals(Z);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }

    public override string ToString() => 
        $"{X} {Y} {Z}";
}