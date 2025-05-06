using System;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PracticeTasks.Services;
using PracticeTasks.Services.Interfaces;
using Moq;
using PracticeTasks.Configuration;
using System.Collections.Generic;

namespace PracticeTasks.Tests;

[TestFixture]
public class StringServiceTests
{
    private StringsService _stringService;

    [SetUp]
    public void Setup()
    {
        var mockSortingService = new Mock<ISortingService>();
        var mockRandomService = new Mock<IRandomService>();
        var options = Options.Create(new AppSettings
        {
            RandomApi = "https://www.randomnumberapi.com/api/v1.0/random",
            Settings = new Settings
            {
                BlackList = new List<string>()
                {
                    "badstring",
                    "blacklisted"
                }
            }
        });
        
        _stringService = new StringsService(mockSortingService.Object, mockRandomService.Object, options);
    }

    [Category("MirrorString")]
    [Test]
    public void MirrorString_WithEvenLength_ReturnsMirroredHalfs()
    {
        string input = "abcd";
        string result = _stringService.MirrorString(input);
        Assert.AreEqual("badc", result);
    }

    [Category("MirrorString")]
    [Test]
    public void MirrorString_WithOddLength_ReturnsReversedAndOriginal()
    {
        string input = "abc";
        string result = _stringService.MirrorString(input);
        Assert.AreEqual("cbaabc", result);
    }

    [Category("ValidateString")]
    [Test]
    public void ValidateString_WithEmptyString_ReturnsException()
    {
        string input = "";
        var ex = Assert.Throws<ArgumentException>(() => _stringService.MirrorString(input));
        Assert.AreEqual("Строка не должна быть пустой", ex.Message);
    }
    
    [Category("ValidateString")]
    [Test]
    public void ValidateString_WithIncorrectSymbols_ReturnsException()
    {
        string input = "1abc2";
        var ex = Assert.Throws<ArgumentException>(() => _stringService.MirrorString(input));
        StringAssert.Contains("В строке присутсвуют невалидные символы", ex.Message);
    }
    
    [Category("ValidateString")]
    [Test]
    public void ValidateString_WithBlackListedString_ReturnsException()
    {
        string input = "badstring";
        var ex = Assert.Throws<ArgumentException>(() => _stringService.MirrorString(input));
        Assert.AreEqual("Данная строка находится в черном списке.", ex.Message);
    }

    [Category("GetCharacterCount")]
    [Test]
    public void GetCharacterCount_WithNormalString_ReturnsCorrectCharacterCount()
    {
        string input = "badc";
        
        var result = _stringService.GetCharacterCount(input);
        
        Assert.AreEqual(1, result['a']);
        Assert.AreEqual(1, result['b']);
        Assert.AreEqual(1, result['c']);
        Assert.AreEqual(1, result['d']);
    }
    
    [Category("GetCharacterCount")]
    [Test]
    public void GetCharacterCount_WithMultipleCharacters_ReturnsCorrectCharacterCount()
    {
        string input = "baab";
        
        var result = _stringService.GetCharacterCount(input);
        
        Assert.AreEqual(2, result['a']);
        Assert.AreEqual(2, result['b']);
    }
    
    [Category("GetLongestVowelSubstring")]
    [Test]
    public void GetLongestVowelSubstring_WithNoVowels_ReturnsNull()
    {
        string input = "bcdf";
        
        var result = _stringService.GetLongestVowelSubstring(input);
        
        Assert.IsNull(result);
    }
    
    [Category("GetLongestVowelSubstring")]
    [Test]
    public void GetLongestVowelSubstring_WithOneVowel_ReturnsOneCharacter()
    {
        string input = "bcydf";
        
        var result = _stringService.GetLongestVowelSubstring(input);
        
        Assert.AreEqual("y", result);
    }
    
    [Category("GetLongestVowelSubstring")]
    [Test]
    public void GetLongestVowelSubstring_WithOneVowelSubstring_ReturnsCorrectSubstring()
    {
        string input = "bacde";
        
        var result = _stringService.GetLongestVowelSubstring(input);
        
        Assert.AreEqual("acde", result);
    }
    
    [Category("GetLongestVowelSubstring")]
    [Test]
    public void GetLongestVowelSubstring_WithMultipleVowelSubstrings_ReturnsLongestSubstring()
    {
        string input = "bacdefghi";
        
        var result = _stringService.GetLongestVowelSubstring(input);
        
        Assert.AreEqual("acdefghi", result);
    }
    
    [Category("GetLongestVowelSubstring")]
    [Test]
    public void GetLongestVowelSubstring_WithOneVowelCharacter_ReturnsCorrectSubstring()
    {
        string input = "a";
        
        var result = _stringService.GetLongestVowelSubstring(input);
        
        Assert.AreEqual("a", result);
    }
    
    [Category("SortString")]
    [Test]
    public void SortString_WithQuickMethod_ReturnsSortedString()
    {
        string input = "badc";
        
        var mockSortingService = new Mock<ISortingService>();
        mockSortingService.Setup(s => s.QuickSort(It.IsAny<char[]>(), 0, It.IsAny<int>()))
            .Callback<char[], int, int>((array, left, right) => Array.Sort(array));
        
        var options = Options.Create(new AppSettings
        {
            RandomApi = "",
            Settings = new Settings
            {
                BlackList = new List<string>()
            }
        });
        
        var stringService = new StringsService(mockSortingService.Object, null, options);
        
        var result = stringService.SortString(input, "quick");
        
        Assert.AreEqual("abcd", result);
    }
    
    [Category("SortString")]
    [Test]
    public void SortString_WithTreeMethod_ReturnsSortedString()
    {
        string input = "badc";
        
        var mockSortingService = new Mock<ISortingService>();
        mockSortingService.Setup(s => s.TreeSort(It.IsAny<char[]>()))
            .Callback<char[]>((array) => Array.Sort(array));
        
        var options = Options.Create(new AppSettings
        {
            RandomApi = "",
            Settings = new Settings
            {
                BlackList = new List<string>()
            }
        });
        
        var stringService = new StringsService(mockSortingService.Object, null, options);
        
        var result = stringService.SortString(input, "tree");
        
        Assert.AreEqual("abcd", result);
    }
    
    [Category("SortString")]
    [Test]
    public void SortString_WithIncorrectMethod_ReturnsException()
    {
        string input = "badc";
        var ex = Assert.Throws<ArgumentException>(() => _stringService.SortString(input, "incorrect"));
        Assert.AreEqual("Такой метод сортировки не реализован.", ex.Message);
    }
}