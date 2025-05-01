namespace PracticeTasks.Services.Interfaces;

public interface IStringsService
{
    string MirrorString(string input);
    
    Dictionary<char, int> GetCharacterCount(string input);
    
    string GetLongestVowelSubstring(string input);
    
    string SortString(string input, string sortMethod);
    
    Task<string> GetStringWithRemovedSymbol(string input);
}