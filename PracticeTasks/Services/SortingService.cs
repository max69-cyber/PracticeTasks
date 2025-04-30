using System.Collections.Concurrent;
using System.Text;
using PracticeTasks.Services.Interfaces;

namespace PracticeTasks.Services;

public class SortingService : ISortingService
{
    public void QuickSort(char[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = Partition(array, left, right);
            QuickSort(array, left, middle - 1);
            QuickSort(array, middle + 1, right);
        }
    }
    
    public void TreeSort(char[] array)
    {
        Node root = null;

        foreach (var c in array)
        {
            root = InsertToTree(root, c);
        }

        int index = 0;
        InorderRec(root, array, ref index);
    }
    
    private int Partition(char[] arr, int left, int right)
    {
        char middleIndex = arr[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j] < middleIndex)
            {
                i++;
                Swap(ref arr[i], ref arr[j]);
            }
        }
        Swap(ref arr[i + 1], ref arr[right]);
        return i + 1;
    }
    
    private void Swap(ref char a, ref char b)
    {
        char temp = a;
        a = b;
        b = temp;
    }
    
    public class Node
    { 
        public char key; 
        public Node left, right; 
  
        public Node(char item)  
        { 
            key = item; 
            left = right = null; 
        } 
    }

    private Node InsertToTree(Node root, char key)
    {
        if (root == null)
        {
            return new Node(key);
        }

        if (key < root.key)
        {
            root.left = InsertToTree(root.left, key);
        }
        else
        {
            root.right = InsertToTree(root.right, key);
        }
        
        return root;
    }

    private void InorderRec(Node node, char[] array, ref int index)
    {
        if (node != null)
        {
            InorderRec(node.left, array, ref index);
            array[index++] = node.key;
            InorderRec(node.right, array, ref index);
        }
    }
}