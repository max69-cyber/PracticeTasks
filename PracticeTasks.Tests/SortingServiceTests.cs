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
public class SortingServiceTests
{
    private SortingService _sortingService;

    [SetUp]
    public void Setup()
    {
        _sortingService = new SortingService();
    }
    
    [Category("QuickSort")]
    [Test]
    public void QuickSort_WithUnsortedArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'd', 'a', 'c', 'b' };
        char[] expected = { 'a','b', 'c', 'd' };
        
        _sortingService.QuickSort(input, 0, input.Length - 1);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("QuickSort")]
    [Test]
    public void QuickSort_WithSortedArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'a','b', 'c', 'd' };
        char[] expected = { 'a','b', 'c', 'd' };
        
        _sortingService.QuickSort(input, 0, input.Length - 1);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("QuickSort")]
    [Test]
    public void QuickSort_WithOneElementArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'a' };
        char[] expected = { 'a' };
        
        _sortingService.QuickSort(input, 0, input.Length - 1);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("QuickSort")]
    [Test]
    public void QuickSort_WithEmptyArray_ReturnsNothing()
    {
        char[] input = { };
        char[] expected = { };
        
        _sortingService.QuickSort(input, 0, input.Length - 1);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("TreeSort")]
    [Test]
    public void TreeSort_WithUnsortedArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'd', 'a', 'c', 'b' };
        char[] expected = { 'a','b', 'c', 'd' };
        
        _sortingService.TreeSort(input);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("TreeSort")]
    [Test]
    public void TreeSort_WithSortedArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'a','b', 'c', 'd' };
        char[] expected = { 'a','b', 'c', 'd' };
        
        _sortingService.TreeSort(input);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("TreeSort")]
    [Test]
    public void TreeSort_WithOneElementArray_ReturnsSortedCorrectly()
    {
        char[] input = { 'a' };
        char[] expected = { 'a' };
        
        _sortingService.TreeSort(input);
        
        Assert.AreEqual(expected, input);
    }
    
    [Category("TreeSort")]
    [Test]
    public void TreeSort_WithEmptyArray_ReturnsNothing()
    {
        char[] input = { };
        char[] expected = { };
        
        _sortingService.TreeSort(input);
        
        Assert.AreEqual(expected, input);
    }
}