using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Shift this to general extensions when needed.
/// Author: Dustin Gruenwald
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    ///   Opposite of <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/>
    /// </summary>
    public static bool None<T>(this IEnumerable<T> items)
    {
        return !items.Any();
    }

    /// <summary>
    ///   Opposite of <see cref="Enumerable.Any{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    /// </summary>
    public static bool None<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        return !items.Any(predicate);
    }

    public static IEnumerable<T> SingleItemAsEnumerable<T>(this T item)
    {
        yield return item;
    }

    /// <summary>
    ///   Performs a FirstOrDefault and returns <see langword="true"/> if item != null
    /// </summary>
    public static bool FirstOrDefault<T>(this IEnumerable<T> items, Func<T, bool> predicate, out T item)
    {
        item = items.FirstOrDefault(predicate);
        return item != null;
    }

    public static bool FirstOrDefault<T, Z>(this Dictionary<T, Z> dictionay, Func<KeyValuePair<T, Z>, bool> predicate, out KeyValuePair<T, Z> item)
    {
        foreach (var pair in dictionay)
        {
            if (predicate(pair))
            {
                item = pair;
                return true;
            }
        }

        item = default;
        return false;
    }

    /// <summary>
    ///   Chains the given items in a single string using default ToString() method and the given seperator.
    /// </summary>
    public static string JoinString<T>(this IEnumerable<T> items, string seperator = ", ")
    {
        return string.Join(seperator, items);
    }

    /// <summary>
    ///   Chains the given items in a single string using a custom ToString() method and the given seperator.
    /// </summary>
    public static string JoinString<T>(this IEnumerable<T> items, Func<T, string> itemToString, string seperator = ", ")
    {
        return string.Join(seperator, items.Select(itemToString));
    }

    /// <summary>
    ///   Finds the index of the first item matching an expression in an enumerable.</summary>
    /// <param name="items">The enumerable to search.</param>
    /// <param name="predicate">The expression to test the items against.</param>
    /// <returns>The index of the first matching item, or -1 if no items match.</returns>
    public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        int index = 0;
        foreach (T item in items)
        {
            if (predicate(item))
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    /// <summary>
    ///   Mean = Sum / Count
    /// </summary>
    public static double Mean(this IEnumerable<double> items)
    {
        return items.Sum() / items.Count();
    }

    public static double Mean<T>(this IEnumerable<T> items, Func<T, double> doubleSelector)
    {
        return items.Sum(doubleSelector) / items.Count();
    }

    /// <summary>
    /// Checks if there are different elements to the <paramref name="list"/>.
    /// Please notify if there is an element twice or more in a list, the count is different but this function returns <see langword="false"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="comparingList">List to compare with.</param>
    /// <returns></returns>
    public static bool AreDifferentElementsIn<T>(this IEnumerable<T> list, IEnumerable<T> comparingList)
    {
        if (list == comparingList)
        {
            return false;
        }
        else if (comparingList == null)
        {
            return list.Any();
        }
        return (list.Except(comparingList).Count() + comparingList.Except(list).Count()) > 0;
    }

    /// <summary>
    /// Returns the index of the first element in the sequence 
    /// that satisfies a condition.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the elements of <paramref name="source"/>.
    /// </typeparam>
    /// <param name="source">
    /// An <see cref="IEnumerable{T}"/> that contains
    /// the elements to apply the predicate to.
    /// </param>
    /// <param name="predicate">
    /// A function to test each element for a condition.
    /// </param>
    /// <returns>
    /// The zero-based index position of the first element of <paramref name="source"/>
    /// for which <paramref name="predicate"/> returns <see langword="true"/>;
    /// or -1 if <paramref name="source"/> is empty
    /// or no element satisfies the condition.
    /// </returns>
    public static int IndexOfWhere<TSource>(this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
    {
        int i = 0;

        foreach (TSource element in source)
        {
            if (predicate(element))
            {
                return i;
            }

            i++;
        }

        return -1;
    }

    public static IEnumerable<int> IndexesOfWhere<TSource>(this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
    {
        int i = 0;

        foreach (TSource element in source)
        {
            if (predicate(element))
            {
                yield return i;
            }

            i++;
        }
    }

    public static IEnumerable<T> Except<T>(this IEnumerable<T> items, T itemToExclude)
    {
        return items.Except(new T[] { itemToExclude });
    }

    public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> items)//, Func<T, TKey> keySelector)
    {
        IEnumerable<T> duplicates = items.GroupBy(x => x)
          .Where(g => g.Count() > 1)
          .Select(y => y.Key);
        return duplicates;

        //var hash = new HashSet<T>();
        //var duplicates = enumerable.Where(i => !hash.Add(i));
        //return duplicates;
    }

    public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
    {
        int i = 0;
        foreach (T element in list)
        {
            if (Equals(element, item))
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    /// <summary>
    ///   Removes "countToDrop" items from the end of the collection.
    /// </summary>
    public static IEnumerable<T> DropLast<T>(this IEnumerable<T> items, int countToDrop)
    {
        Queue<T> buffer = new Queue<T>(countToDrop + 1);

        foreach (T item in items)
        {
            buffer.Enqueue(item);

            if (buffer.Count == countToDrop + 1)
            {
                yield return buffer.Dequeue();
            }
        }
    }
}