using System;

public static class IntExtensions
{
    public static double Pow(this int value, double exponent)
    {
        return Math.Pow(value, exponent);
    }
}
