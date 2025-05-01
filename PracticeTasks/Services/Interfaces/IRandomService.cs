namespace PracticeTasks.Services.Interfaces;

public interface IRandomService
{
    Task<int> GetRandomNumber(int max);
}