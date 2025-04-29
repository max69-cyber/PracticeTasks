using System.Text;
using PracticeTasks.Services.Interfaces;

namespace PracticeTasks.Services;

public class StringsService : IStringsService
{
    public string MirrorString(string input)
    {
        ValidateString(input);
        
        var result = new StringBuilder();
        int length = input.Length;
        
        if (length % 2 == 0)
        {
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }
            for (int i = length - 1; i >= length / 2; i--)
            {
                result.Append(input[i]);
            }
        }
        else
        {
            for (int i = length - 1; i >= 0; i--)
            {
                                
                result.Append(input[i]);
            }
            result.Append(input);
        }
        
        return result.ToString();
    }

    public Dictionary<char, int> GetCharacterCount(string input)
    {
        ValidateString(input);
        
        var charCount = input
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
        
        return charCount;
    }

    public string GetLongestVowelSubstring(string input)
    {
        var vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        int longestVowelSubstring = 0;
        int startIndex = -1;
        int latestVowelIndex = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if (vowels.Contains(input[i]))
            {
                if (latestVowelIndex != -1)
                {
                    int currentVowelSubstring = i - latestVowelIndex - 1;
                    if (currentVowelSubstring > longestVowelSubstring)
                    {
                        longestVowelSubstring = currentVowelSubstring;
                        startIndex = latestVowelIndex;
                    }
                }
                latestVowelIndex = i;
            }
        }

        if (startIndex == -1) return null;
        
        return input.Substring(startIndex, longestVowelSubstring);
    }

    private void ValidateString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Строка не должна быть пустой");
        }
        
        var errorSymbols = input
            .Where(c => c < 'a' || c > 'z')
            .ToList();
        
        if (errorSymbols.Any())
        {
            var errorString = new StringBuilder("В строке присутсвуют невалидные символы: ");
            foreach (var c in errorSymbols)
            {
                errorString.Append($"{c}, ");
            }
            
            errorString.Remove(errorString.Length - 2, 2);
            errorString.Append(".");
            
            throw new ArgumentException(errorString.ToString());
        }
    }
}