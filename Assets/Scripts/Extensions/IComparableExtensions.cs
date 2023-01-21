using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IComparableExtensions
{
    /// <summary>
    ///   Returns a value that is not smaller then min and not greater then max.
    /// </summary>
    public static T Clamp<T>(this T value, T min, T max)
        where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
        {
            return min;
        }
        else if (value.CompareTo(max) > 0)
        {
            return max;
        }
        return value;
    }

    /// <summary>
    ///   Returns a value that is not smaller then min.
    /// </summary>
    public static T ClampMin<T>(this T value, T min)
        where T : IComparable<T>
    {
        return value.CompareTo(min) < 0 ? min : value;
    }

    /// <summary>
    ///   Returns a value that is not greater then max.
    /// </summary>
    public static T ClampMax<T>(this T value, T max)
        where T : IComparable<T>
    {
        return value.CompareTo(max) > 0 ? max : value;
    }
}