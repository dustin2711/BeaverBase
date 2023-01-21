using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StringUtilities
{
    public static string CreateUniqueName(string baseName, IEnumerable<string> takenNames)
    {
        string endingNumberString = string.Concat(baseName.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
        if (int.TryParse(endingNumberString, out int number))
        {
            // Ends with number? Remove number
            baseName = baseName.Substring(0, baseName.LastIndexOf(endingNumberString));
        }
        else
        {
            number = 0;
        }
        while (true)
        {
            string currentName = baseName + (number > 0 ? number.ToString() : "");
            if (!takenNames.Contains(currentName))
            {
                return currentName;
            }
            number++;
        }
    }

    public static string ReplaceLastOccurrence(this string text, string toReplace, string replacement)
    {
        int place = text.LastIndexOf(toReplace);

        if (place == -1)
        {
            return text;
        }

        string result = text.Remove(place, toReplace.Length).Insert(place, replacement);
        return result;
    }
}