using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObjectExtensions
{
    /// <summary>
    ///   Is item equal to one of other items?
    /// </summary>
    public static bool IsEqualToOne<T>(this T item, params T[] items)
    {
        foreach (T item2 in items)
        {
            if (item.Equals(item2))
            {
                return true;
            }
        }
        return false;
    }


    //public static T Get<T>(this IEnumerable<T> items) where T : IEnumerable<T>
    //{

    //}
}