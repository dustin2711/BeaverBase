using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class EnumExtensions
{
    ///// <summary>
    /////   Gets the "Description" attribute string from an Enum value.
    ///// </summary>
    //public static string GetDescription(this Enum value)
    //{
    //    Type type = value.GetType();
    //    string name = Enum.GetName(type, value);
    //    if (name != null)
    //    {
    //        FieldInfo field = type.GetField(name);
    //        if (field != null)
    //        {
    //            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
    //            if (attribute != null)
    //            {
    //                return attribute.Description;
    //            }
    //        }
    //    }
    //    return value.ToString();
    //}

    /// <summary>
    ///   Gets the next value in the enumeration.
    ///   Repeats from beginning or clamps at end.
    /// </summary>
    public static T Next<T>(this T value, bool repeatAtEnd = true) where T : Enum
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));
        }

        T[] values = (T[])Enum.GetValues(value.GetType());
        int index = Array.IndexOf(values, value) + 1;
        if (repeatAtEnd)
        {
            return (values.Length == index) ? values[0] : values[index];
        }
        else
        {
            return values[index.Clamp(0, values.Length - 1)];
        }
    }

    /// <summary>
    ///   Returns the text from the description attribute. If attribute does not exist, returns the enum.ToString().
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        FieldInfo info = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Returns all defined values from a flagged enum of the given type of enum.
    /// </summary>
    /// <typeparam name="T">Enum type.</typeparam>
    /// <param name="e">Enum</param>
    /// <returns></returns>
    static public IEnumerable<T> GetAllEnumValuesFromFlagging<T>(this T e) where T : struct
    {
        foreach (var val in Enum.GetValues(typeof(T)).Cast<T>())
        {
            if ((Convert.ToInt32(val) & Convert.ToInt32(e)) != 0)
            {
                yield return val;
            }
        }
    }
}