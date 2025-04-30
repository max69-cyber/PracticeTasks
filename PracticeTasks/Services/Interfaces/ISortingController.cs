namespace PracticeTasks.Services.Interfaces;

public interface ISortingService
{
    void QuickSort(char[] array, int left, int right);
    
    void TreeSort(char[] array);
}