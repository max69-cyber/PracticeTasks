using System.Text;
using Microsoft.Extensions.Options;
using PracticeTasks.Configuration;
using PracticeTasks.Services.Interfaces;

namespace PracticeTasks.Services;

public class StringsService : IStringsService
{
    private ISortingService _sortingService;
    private IRandomService _randomService;
    private readonly List<string> _blacklist;

    public StringsService(ISortingService sortingService, IRandomService randomService, IOptions<AppSettings> appSettings)
    {
        _sortingService = sortingService;
        _randomService = randomService;
        _blacklist = appSettings.Value.Settings.BlackList;
    }
    
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
                    int currentVowelSubstring = i + 1 - latestVowelIndex;
                    if (currentVowelSubstring > longestVowelSubstring)
                    {
                        longestVowelSubstring = currentVowelSubstring;
                        startIndex = latestVowelIndex;
                    }
                }
                else
                {
                    latestVowelIndex = i;
                }
            }
        }

        if (latestVowelIndex == -1) return null;
        if (startIndex == -1) return input.Substring(latestVowelIndex,1);
        
        return input.Substring(startIndex, longestVowelSubstring);
    }

    public string SortString(string input, string sortMethod)
    {
        if (sortMethod == "quick")
        {
            char[] array = input.ToCharArray();
            _sortingService.QuickSort(array, 0, array.Length - 1);
            string sortingResult = new string(array);
            
            return sortingResult;
        }
        else if (sortMethod == "tree")
        {
            char[] array = input.ToCharArray();
            _sortingService.TreeSort(array);
            string sortingResult = new string(array);
            
            return sortingResult;
        }
        else
        {
            throw new ArgumentException("Такой метод сортировки не реализован.");
        }
        
    }

    public async Task<string> GetStringWithRemovedSymbol(string input)
    {
        int length = input.Length;
        int index = await _randomService.GetRandomNumber(length);

        return input.Remove(index, 1);
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

        if (_blacklist.Contains(input)) 
            throw new ArgumentException("Данная строка находится в черном списке.");
    }
}