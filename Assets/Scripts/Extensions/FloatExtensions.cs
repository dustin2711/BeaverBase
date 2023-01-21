using System;

public static class FloatExtensions
{
    public const float Pi = (float)Math.PI;

    public static float Pow(this float value, float exponent)
    {
        return (float)Math.Pow(value, exponent);
    }

    public static float ToRadian(this float value)
    {
        return value * Pi / 180f;
    }

    public static float ToDegrees(this float value)
    {
        return value * 180f / Pi;
    }

    /// <summary>
    ///   Clamps angle to 0 and 2*Pi in radian.
    /// </summary>
    public static float NormalizeAngle0To360(this float angle)
    {
        float normalized = angle % 360;

        if (normalized < 0)
        {
            normalized += 360;
        }

        return normalized;
    }

    public static float NormalizeAngleMinus360To0(this float angle)
    {
        float normalized = angle % 360;

        if (normalized > 0)
        {
            normalized -= 360;
        }

        return normalized;
    }

    public static float NormalizeAngle180(this float angle)
    {
        float normalized = angle % 360;

        if (normalized < 180)
        {
            normalized += 360;
        }

        if (normalized > 180)
        {
            normalized -= 360;
        }

        return normalized;
    }

    public static string ToShortString(this float value, string format = "0.00")
    {
        return value.ToString(format);
    }

    public static string ToString(this float value, int maxDecimalPlaces)
    {
        if (float.IsNaN(value))
        {
            return "NaN";
        }

        int precision = 0;
        while (value * Math.Pow(10, precision) != Math.Round(value * Math.Pow(10, precision))
            && precision < maxDecimalPlaces)
        {
            precision++;
        }
        return value.ToString("0." + new string('0', precision));
    }

    public static int Sign(this float value)
    {
        return Math.Sign(value);
    }

    public static bool IsPositive(this float value)
    {
        return value > 0;
    }

    public static bool IsNegative(this float value)
    {
        return value < 0;
    }

    public static int ToInt(this float value)
    {
        return Convert.ToInt32(value);
    }

    /// <summary>
    ///   E.g. round to 0.001.
    /// </summary>
    public static float Round(this float value, float roundTo)
    {
        return (float)(Math.Round(value / roundTo) * roundTo);
    }

    public static bool IsAbout(this float value, float other, float epsilon = 0.000001f)
    {
        return Math.Abs(value - other) < epsilon;
    }

    public static float Abs(this float value)
    {
        return Math.Abs(value);
    }
}