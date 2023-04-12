// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using System.Text;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");



var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();



[MemoryDiagnoser]
public class MemoryBenchmarkerDemo
{
    int[] arr = CreateArray();
    [Benchmark]
    public int[] BubbleSort() => BubbleSort(arr);
    [Benchmark]
    public int[] MergeSort() => MergeSort(arr);

    static int[] CreateArray()
    {
        Random random = new Random();
        int[] arr = new int[10000];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = random.Next(10000);
        }
        return arr;
    }

    static int[] BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // swap arr[j] and arr[j+1]
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }

    static int[] MergeSort(int[] arr)
    {
        if (arr.Length > 1)
        {
            int mid = arr.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[arr.Length - mid];

            // Copy the left half of arr to the left array
            for (int i = 0; i < mid; i++)
            {
                left[i] = arr[i];
            }

            // Copy the right half of arr to the right array
            for (int i = mid; i < arr.Length; i++)
            {
                right[i - mid] = arr[i];
            }

            // Recursively sort the left and right arrays
            MergeSort(left);
            MergeSort(right);

            // Merge the sorted left and right arrays
            Merge(arr, left, right);
        }
        return arr;
    }

    static int[] Merge(int[] arr, int[] left, int[] right)
    {
        int i = 0, j = 0, k = 0;

        // Merge the left and right arrays into the original array
        while (i < left.Length && j < right.Length)
        {
            if (left[i] < right[j])
            {
                arr[k] = left[i];
                i++;
            }
            else
            {
                arr[k] = right[j];
                j++;
            }
            k++;
        }

        // Copy the remaining elements of left array, if any
        while (i < left.Length)
        {
            arr[k] = left[i];
            i++;
            k++;
        }

        // Copy the remaining elements of right array, if any
        while (j < right.Length)
        {
            arr[k] = right[j];
            j++;
            k++;
        }
        return arr;
    }
}