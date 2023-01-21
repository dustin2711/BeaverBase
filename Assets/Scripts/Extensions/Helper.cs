using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class Helper
{
    /// <summary>
    ///   One quarter rotation in radian (pi/2).
    /// </summary>
    public const double Quarter = 0.5 * Math.PI;

    /// <summary>
    ///   Return a new Stopwatch with <see cref="Stopwatch.Start"/> already called.
    /// </summary>
    public static Stopwatch GetStartedStopwatch()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        return watch;
    }

#if DEBUG
    private static Stopwatch watch = new Stopwatch();

    /// <summary>
    ///   Starts Helper-Stopwatch and returns elapsed millisecond since last start.
    /// </summary>
    public static double StartStopwatch()
    {
        watch.Restart();
        return watch.Elapsed.TotalMilliseconds;
    }

    /// <summary>
    ///   Stops Helper-Stopwatch and returns elapsed millisecond.
    /// </summary>
    public static double StopStopwatch()
    {
        watch.Stop();
        return watch.Elapsed.TotalMilliseconds;
    }
#endif

    /// <summary>
    ///   Gets the <typeparamref name="T"/> attribute of the given type.
    /// </summary>
    public static bool GetAttribute<T>(Type type, out T attribute) where T : Attribute
    {
        attribute = GetAttribute<T>(type);
        return attribute != null;
    }

    /// <summary>
    ///   Returns true if the type is Nullable
    /// </summary>
    public static bool IsTypeNullable(Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }

    /// <summary>
    ///   Gets the <typeparamref name="T"/> attribute of the given type.
    /// </summary>
    public static T GetAttribute<T>(Type type) where T : Attribute
    {
        return type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
    }

    public static T[] CopyArray<T>(T[] array, int length = -1)
    {
        if (array == null)
        {
            return null;
        }

        if (length == -1)
        {
            length = array.Length;
        }
        else
        {
            length = Math.Min(array.Length, length);
        }

        T[] copy = new T[length];
        Array.Copy(array, copy, length);
        //array.CopyTo(copy, 0);
        return copy;
    }


    /// <summary>
    ///   For input ("item", 5) => returns "5 items"
    /// </summary>
    public static string MakePlural(int count, string text)
    {
        return count + " " + text + (count != 1 ? "s" : "");
    }

    public static string MakePlural(string textSingular, string textPlural, int count)
    {
        return count + " " + (count != 1 ? textPlural : textSingular);
    }

    /// <summary>
    ///   Filter number from a string by chaining all occuring digits.
    /// </summary>
    public static bool FilterNumber(string text, out int number)
    {
        return int.TryParse(new string(text.Where(char.IsDigit).ToArray()), out number);
    }

    /// <summary>
    ///   Returns a string with all numbers removed.
    /// </summary>
    public static string RemoveNumbers(string text)
    {
        return new string(text.Where(c => !char.IsNumber(c)).ToArray());
    }

    /// <summary>
    ///   Get all numbers from a string as string array
    /// </summary>
    public static string[] FilterNumbersString(string text)
    {
        string[] stringNumbers = Regex.Split(text, @"\D+");
        return stringNumbers;
    }

    /// <summary>
    ///   Get all numbers from a string as int array
    /// </summary>
    public static int[] FilterNumbers(string text)
    {
        string[] stringNumbers = FilterNumbersString(text);
        int[] numbers = stringNumbers.Where(it => it.Length > 0).Select(it => int.Parse(it)).ToArray();
        return numbers;
    }

    public static string FillNumberWithZeros(int number, int maxNumber)
    {
        return new string('0', maxNumber.ToString().Length - number.ToString().Length) + number;
    }

    /// <summary>
    ///   Returns all names of public constant fields in a class.
    /// </summary>
    public static List<string> GetConstantFieldNames(Type type)
    {
        FieldInfo[] infos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        return infos.Where(it => it.IsLiteral && !it.IsInitOnly).Select(it => it.Name).ToList();
    }

    /// <summary>
    ///   Returns all names of public static properties in a class.
    /// </summary>
    public static List<string> GetPublicStaticPropertyNames(Type type)
    {
        PropertyInfo[] infos = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
        return infos.Select(it => it.Name).ToList();
    }

    /// <summary>
    ///   Is the event with the given name subscribed at least once at the given object?
    /// </summary>
    public static bool IsEventSubscribed<T>(T instance, string nameOfEvent)
    {
        return typeof(T).GetField(nameOfEvent,
            BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance) is Delegate;
    }

    public static void SetPropertyByReflection<T>(T obj, string propertyName, object value)
    {
        PropertyInfo info = typeof(T).GetProperty(propertyName);
        if (info?.CanWrite == true)
        {
            info.SetValue(obj, value);
        }
    }

    public static void SetFieldByReflection<T>(T obj, string fieldName, object value, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance)
    {
        FieldInfo info = typeof(T).GetField(fieldName, flags);
        info.SetValue(obj, value);
    }

    public static bool FindFirstIndexNotInList(List<int> indexes, int countBeforeAbort, out int indexNotInList)
    {
        for (indexNotInList = 0; indexNotInList < countBeforeAbort; indexNotInList++)
        {
            if (!indexes.Contains(indexNotInList))
            {
                return true;

            }
        }

        return false;
    }

    public static object GetFieldByReflection<T>(T obj, string fieldName, BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance)
    {
        FieldInfo info = typeof(T).GetField(fieldName, flags);
        object value = info.GetValue(obj);
        return value;
    }

    ///// <summary>
    /////   Compares if two Vector2-Lists have identical points (with a given tolerance )
    ///// </summary>
    //public static bool PointlistsAreSame(List<Vector2> listA, List<Vector2> listB, double allowedDelta = 0.0001)
    //{
    //    bool PointsAreAboutSame(Vector2 a, Vector2 b) 
    //        => Math.Abs(a.x - b.x) < allowedDelta
    //        && Math.Abs(a.y - b.y) < allowedDelta;

    //    Func<Vector2, Vector2, bool> pointsAreSame = (allowedDelta == 0)
    //        ? new Func<Vector2, Vector2, bool>((a, b) => a.ValueEquals(b)) 
    //        : PointsAreAboutSame;

    //    if (listB.Count != listA.Count)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        for (int i = 0; i < listB.Count; i++)
    //        {
    //            if (!pointsAreSame(listA[i], listB[i]))
    //            {
    //                return false;
    //            }
    //        }
    //    }

    //    return true;
    //}

    public static bool ListsAreSame<T>(List<T> a, List<T> b) where T : struct
    {
        if (a.Count != b.Count)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }


    /// <summary>
    ///   Compute the Levenshtein distance between two strings.
    /// </summary>
    public static int GetLevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // Step 1
        if (n == 0)
        {
            return m;
        }
        else if (m == 0)
        {
            return n;
        }

        // Step 2 (Initialize array)
        for (int i = 0; i <= n; d[i, 0] = i++) { }
        for (int j = 0; j <= m; d[0, j] = j++) { }

        // Step 3
        for (int i = 1; i <= n; i++)
        {
            //Step 4
            for (int j = 1; j <= m; j++)
            {
                // Step 5
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                // Step 6
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        // Step 7
        return d[n, m];
    }

}