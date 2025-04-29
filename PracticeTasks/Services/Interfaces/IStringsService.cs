namespace PracticeTasks.Services.Interfaces;

public interface IStringsService
{
    string MirrorString(string input);
    
    Dictionary<char, int> GetCharacterCount(string input);
    
    string GetLongestVowelSubstring(string input);
}