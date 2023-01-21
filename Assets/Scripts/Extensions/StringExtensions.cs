using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static bool EndsWithAny(this string text, params string[] endings)//, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        return endings.Any(end => text.EndsWith(end, StringComparison.OrdinalIgnoreCase));
    }

    public static string RemoveEnd(this string text, string[] toRemove)
    {
        foreach (string item in toRemove)
        {
            if (text.EndsWith(item))
            {
                text = text.Substring(0, text.LastIndexOf(item));
                break; //only allow one match at most
            }
        }
        return text;
    }

    public static bool IsNullOrWhitespace(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static bool IsNullOrEmpty(this string text)
    {
        return string.IsNullOrEmpty(text);
    }

    public static string RemoveFromEnd(this string text, string toRemove)
    {
        if (text.EndsWith(toRemove))
        {
            return text.Substring(0, text.Length - toRemove.Length);
        }
        else
        {
            return text;
        }
    }

    public static bool Contains(this string source, string content, StringComparison comparison)
    {
        return source?.IndexOf(content, comparison) >= 0;
    }

    public static bool ContainsAny(this string text, params string[] itemsToCheck)
    {
        foreach (string item in itemsToCheck)
        {
            if (text.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainsAny(this string text, params char[] itemsToCheck)
    {
        foreach (char item in itemsToCheck)
        {
            if (text.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    public static string Remove(this string text, params string[] itemsToRemove)
    {
        foreach (string item in itemsToRemove)
        {
            text = text.Replace(item, "");
        }
        return text;
    }

    /// <summary>
    ///   Equals Substring(0, length) without Exception when string is too short
    /// </summary>
    public static string Truncate(this string value, int length)
    {
        return value?.Length > length ? value.Substring(0, length)
                                      : value;
    }

    public static List<string> GetSubstrings(this string input, string start, string end)
    {
        List<string> substrings = new List<string>();
        Regex rx = new Regex(Regex.Escape(start) + "([^}]*)" + Regex.Escape(end));
        foreach (Match match in rx.Matches(input))
            substrings.Add(match.Groups[1].Value);
        return substrings;
    }

    public static List<int> AllIndexesOf(this string str, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("the string to find may not be empty", nameof(value));
        }

        List<int> indexes = new List<int>();
        for (int index = 0; ; index += value.Length)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
            {
                return indexes;
            }
            indexes.Add(index);
        }
    }

    public static int NthIndexOf(this string str, char character, int n)
    {
        if (n < 1)
            throw new ArgumentException($"{nameof(n)} must be greater than zero");

        int idx = 0;
        int count = 0;
        while (idx < str.Length)
        {
            if (str[idx] == character)
            {
                count++;
                if (count == n)
                    return idx;
            }
            idx++;
        }

        return -1;
    }

    /// <summary>
    ///   Returns all lines of the string using StringReader.
    /// </summary>
    public static List<string> GetLines(this string text)
    {
        List<string> lines = new List<string>();
        using (StringReader reader = new StringReader(text))
        {
            while (true)
            {
                string currentLine = reader.ReadLine();
                if (currentLine == null)
                {
                    break;
                }
                lines.Add(currentLine);
            }
        }
        return lines;
    }

    /// <summary>
    ///   Returns true if the text ends with any of the given items.
    /// </summary>
    public static bool EndsWithAny(this string text, IEnumerable<string> items)
    {
        foreach (string item in items)
        {
            if (text.EndsWith(item))
            {
                return true;
            }
        }

        return false;
    }


    ///// <summary>
    ///// Verifies whether the given expression matches the given string. Supports the following wildcards: '*'
    ///// </summary>
    ///// <param name="s">The string to check against the expression.</param>
    ///// <param name="pattern">The expression to match with.</param>
    ///// <param name="caseSensitive"></param>
    ///// <returns><see langword="true" /> if the given expression matches the given string; otherwise, <see langword="false" />.</returns>
    //public static bool IsLike(this string s, string pattern, bool caseSensitive = true)
    //{
    //    return FileSystemName.MatchesSimpleExpression(pattern.AsSpan(), s.AsSpan(), !caseSensitive);
    //}

    /// <summary>
    ///   Returns true if text equals any element
    /// </summary>
    /// <returns></returns>
    public static bool Equals(this string text, params string[] stringsToCompareWith)
    {
        foreach (string element in stringsToCompareWith)
        {
            if (text.Equals(element))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Splits the text by extracting strings NOT wrapped by <paramref name="wrappingChar"/>. Does not work for
    /// nested wrapping. Returns empty list if the opening and closing elements were unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="wrappingChar"></param>
    public static List<string> ExtractUnwrappedStrings(this string text, char wrappingChar)
        => ExtractUnwrappedStrings(text, wrappingChar, wrappingChar);

    /// <summary>
    /// Splits the text by extracting strings NOT wrapped by <paramref name="openingChar"/> and <paramref
    /// name="closingChar"/>. Does not work for nested wrapping. Returns empty list if the opening and closing
    /// elements were unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="openingChar"></param>
    /// <param name="closingChar"></param>
    public static List<string> ExtractUnwrappedStrings(this string text, char openingChar, char closingChar)
    {
        return ExtractWrappedAndUnwrappedStrings(text, openingChar, closingChar).Where(t => !t.Item2).Select(t => t.Item1).ToList();
    }

    /// <summary>
    /// Splits the text by extracting strings wrapped by <paramref name="wrappingChar"/>. Does not work for nested
    /// wrapping. Returns empty list if the opening and closing elements were unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="wrappingChar"></param>
    public static List<string> ExtractWrappedStrings(this string text, char wrappingChar)
        => ExtractWrappedStrings(text, wrappingChar, wrappingChar);

    /// <summary>
    /// Splits the text by extracting strings wrapped by <paramref name="openingChar"/> and <paramref
    /// name="closingChar"/>. Does not work for nested wrapping. Returns empty list if the opening and closing
    /// elements were unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="openingChar"></param>
    /// <param name="closingChar"></param>
    /// <returns></returns>
    public static List<string> ExtractWrappedStrings(this string text, char openingChar, char closingChar)
    {
        return ExtractWrappedAndUnwrappedStrings(text, openingChar, closingChar).Where(t => t.Item2).Select(t => t.Item1).ToList();
    }

    /// <summary>
    /// Splits the text by extracting strings wrapped by <paramref name="wrappingChar"/>. Returns both wrapped and
    /// unwrapped strings indicated by the second tuple argument. True for wrapped strings and False for unwrapped
    /// strings. Does not work for nested wrapping. Returns empty list if the opening and closing elements were
    /// unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="wrappingChar"></param>
    /// <returns></returns>
    public static List<(string, bool)> ExtractWrappedAndUnwrappedStrings(this string text, char wrappingChar)
        => ExtractWrappedAndUnwrappedStrings(text, wrappingChar, wrappingChar);

    /// <summary>
    /// Splits the text by extracting strings wrapped by <paramref name="openingChar"/> and <paramref
    /// name="closingChar"/>. Returns both wrapped and unwrapped strings indicated by the second tuple argument.
    /// True for wrapped strings and False for unwrapped strings. Does not work for nested wrapping. Returns empty
    /// list if the opening and closing elements were unbalanced.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="openingChar"></param>
    /// <param name="closingChar"></param>
    /// <returns></returns>
    public static List<(string, bool)> ExtractWrappedAndUnwrappedStrings(this string text, char openingChar, char closingChar)
    {
        List<(string, bool)> result = new List<(string, bool)>();
        int idx = 0;
        bool opened = false;
        while (idx < text.Length)
        {
            int splitIdx = text.IndexOf(opened ? closingChar : openingChar, idx);
            if (splitIdx < text.Length && splitIdx >= 0)
            {

                string subString = text.Substring(idx, splitIdx - idx);
                idx = splitIdx + 1;
                opened = !opened;
                if (subString != string.Empty)
                {
                    result.Add((subString, !opened));
                }
            }
            else
            {
                string subString = text.Substring(idx, text.Length - idx);
                result.Add((subString, false));
                break;
            }
        }

        if (opened)
        {
            // unbalanced quotes, just return original
            result.Clear();
        }
        return result;
    }

    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another 
    /// specified string according the type of search to use for the specified string.
    /// </summary>
    /// <param name="str">The string performing the replace method.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string replace all occurrences of <paramref name="oldValue"/>. 
    /// If value is equal to <c>null</c>, than all occurrences of <paramref name="oldValue"/> will be removed from the <paramref name="str"/>.</param>
    /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
    /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue"/> are replaced with <paramref name="newValue"/>. 
    /// If <paramref name="oldValue"/> is not found in the current instance, the method returns the current instance unchanged.</returns>
    /// <remarks>Taken from https://stackoverflow.com/questions/6275980/string-replace-ignoring-case </remarks>
    [DebuggerStepThrough]
    public static string Replace(this string str,
        string oldValue, string @newValue,
        StringComparison comparisonType)
    {

        // Check inputs.
        if (str == null)
        {
            // Same as original .NET C# string.Replace behavior.
            throw new ArgumentNullException(nameof(str));
        }
        if (str.Length == 0)
        {
            // Same as original .NET C# string.Replace behavior.
            return str;
        }
        if (oldValue == null)
        {
            // Same as original .NET C# string.Replace behavior.
            throw new ArgumentNullException(nameof(oldValue));
        }
        if (oldValue.Length == 0)
        {
            // Same as original .NET C# string.Replace behavior.
            throw new ArgumentException("String cannot be of zero length.");
        }


        //if (oldValue.Equals(newValue, comparisonType))
        //{
        //This condition has no sense
        //It will prevent method from replacesing: "Example", "ExAmPlE", "EXAMPLE" to "example"
        //return str;
        //}



        // Prepare string builder for storing the processed string.
        // Note: StringBuilder has a better performance than String by 30-40%.
        StringBuilder resultStringBuilder = new StringBuilder(str.Length);



        // Analyze the replacement: replace or remove.
        bool isReplacementNullOrEmpty = string.IsNullOrEmpty(@newValue);



        // Replace all values.
        const int valueNotFound = -1;
        int foundAt;
        int startSearchFromIndex = 0;
        while ((foundAt = str.IndexOf(oldValue, startSearchFromIndex, comparisonType)) != valueNotFound)
        {

            // Append all characters until the found replacement.
            int @charsUntilReplacment = foundAt - startSearchFromIndex;
            bool isNothingToAppend = @charsUntilReplacment == 0;
            if (!isNothingToAppend)
            {
                resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilReplacment);
            }



            // Process the replacement.
            if (!isReplacementNullOrEmpty)
            {
                resultStringBuilder.Append(@newValue);
            }


            // Prepare start index for the next search.
            // This needed to prevent infinite loop, otherwise method always start search 
            // from the start of the string. For example: if an oldValue == "EXAMPLE", newValue == "example"
            // and comparisonType == "any ignore case" will conquer to replacing:
            // "EXAMPLE" to "example" to "example" to "example" … infinite loop.
            startSearchFromIndex = foundAt + oldValue.Length;
            if (startSearchFromIndex == str.Length)
            {
                // It is end of the input string: no more space for the next search.
                // The input string ends with a value that has already been replaced. 
                // Therefore, the string builder with the result is complete and no further action is required.
                return resultStringBuilder.ToString();
            }
        }


        // Append the last part to the result.
        int @charsUntilStringEnd = str.Length - startSearchFromIndex;
        resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilStringEnd);


        return resultStringBuilder.ToString();

    }
}
