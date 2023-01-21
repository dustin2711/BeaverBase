using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DoubleExtensions
{
    public static string ToString(this double value, int maxDecimalPlaces)
    {
        if (double.IsNaN(value))
        {
            return "NaN";
        }

        int precision = 0;
        while (value * Math.Pow(10, precision) != Math.Round(value * Math.Pow(10, precision)))
        {
            precision++;
        }
        return value.ToString("0." + new string('0', precision.Clamp(0, maxDecimalPlaces)));
    }

    public static int ToInt(this double value)
    {
        return Convert.ToInt32(value);
    }

    /// <summary>
    ///   E.g. round to 0.001.
    /// </summary>
    public static double Round(this double value, double roundTo)
    {
        return (double)(Math.Round(value / roundTo) * roundTo);
    }

    public static double Pow(this double value, double exponent)
    {
        return Math.Pow(value, exponent);
    }

    public static double Root(this double value, double root)
    {
        return Math.Pow(value, 1.0 / root);
    }

    public static bool IsAbout(this double value, double other, double epsilon = 0.000001)
    {
        return Math.Abs(value - other) < epsilon;
    }
}