using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using PracticeTasks.Services.Interfaces;

namespace PracticeTasks.Services;

public class RandomService : IRandomService
{
    private readonly HttpClient _httpClient;

    public RandomService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<int> GetRandomNumber(int maxNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://www.randomnumberapi.com/api/v1.0/random?min=0&max={maxNumber}&count=1");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(json);
            
            return numbers?.FirstOrDefault() ?? GetFallbackRandomNumber(maxNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return GetFallbackRandomNumber(maxNumber);
        }
    }

    private int GetFallbackRandomNumber(int maxNumber)
    {
        if (maxNumber <= 0)
            throw new ArgumentOutOfRangeException("Верхняя граница генерации чисел не может быть <= 0");

        return Random.Shared.Next(maxNumber);
    }
}