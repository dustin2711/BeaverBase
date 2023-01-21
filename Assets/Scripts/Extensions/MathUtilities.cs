using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MathUtilities
{
    // Integer - Max

    /// <summary>
    ///   Returns the greater of two numbers. Exists for consistency.
    /// </summary>
    public static int Max(int x, int y)
    {
        return Math.Max(x, y);
    }

    /// <summary>
    ///   Returns the greatest of three numbers.
    /// </summary>
    public static int Max(int x, int y, int z)
    {
        return Math.Max(x, Math.Max(y, z));
    }

    /// <summary>
    ///   Returns the greatest of four numbers.
    /// </summary>
    public static int Max(int w, int x, int y, int z)
    {
        return Math.Max(w, Math.Max(x, Math.Max(y, z)));
    }

    /// <summary>
    ///   Returns the greatest of x numbers.
    /// </summary>
    public static int Max(params int[] values)
    {
        return Enumerable.Max(values);
    }

    // Integer - Min

    /// <summary>
    ///   Returns the smaller of two numbers. Exists for consistency.
    /// </summary>
    public static int Min(int x, int y)
    {
        return Math.Min(x, y);
    }

    /// <summary>
    ///   Returns the smallest of three numbers.
    /// </summary>
    public static int Min(int x, int y, int z)
    {
        return Math.Min(x, Math.Min(y, z));
    }

    /// <summary>
    ///   Returns the smallest of four numbers.
    /// </summary>
    public static int Min(int w, int x, int y, int z)
    {
        return Math.Min(w, Math.Min(x, Math.Min(y, z)));
    }

    /// <summary>
    ///   Returns the smallest of x numbers.
    /// </summary>
    public static int Min(params int[] values)
    {
        return Enumerable.Min(values);
    }

    // Double - Max

    /// <summary>
    ///   Returns the greater of two numbers. Exists for consistency.
    /// </summary>
    public static double Max(double x, double y)
    {
        return Math.Max(x, y);
    }

    /// <summary>
    ///   Returns the greatest of three numbers.
    /// </summary>
    public static double Max(double x, double y, double z)
    {
        return Math.Max(x, Math.Max(y, z));
    }

    /// <summary>
    ///   Returns the greatest of four numbers.
    /// </summary>
    public static double Max(double w, double x, double y, double z)
    {
        return Math.Max(w, Math.Max(x, Math.Max(y, z)));
    }

    /// <summary>
    ///   Returns the greatest of x numbers.
    /// </summary>
    public static double Max(params double[] values)
    {
        return Enumerable.Max(values);
    }

    // Double - Min

    /// <summary>
    ///   Returns the smaller of two numbers. Exists for consistency.
    /// </summary>
    public static double Min(double x, double y)
    {
        return Math.Min(x, y);
    }

    /// <summary>
    ///   Returns the smallest of three numbers.
    /// </summary>
    public static double Min(double x, double y, double z)
    {
        return Math.Min(x, Math.Min(y, z));
    }

    /// <summary>
    ///   Returns the smallest of four numbers.
    /// </summary>
    public static double Min(double w, double x, double y, double z)
    {
        return Math.Min(w, Math.Min(x, Math.Min(y, z)));
    }

    /// <summary>
    ///   Returns the smallest of x numbers.
    /// </summary>
    public static double Min(params double[] values)
    {
        return Enumerable.Min(values);
    }

    public static double RoundToHalf(this double value)
    {
        return Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
    }
}