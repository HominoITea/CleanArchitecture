using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Models;
using Core.Entities.Models.Columns;

namespace Infrastructure.Services
{
    public class RadixSort
    {

        static int GetMax(byte[] a, int n)
        {
            int max = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            return max; //maximum element from the array  
        }

        //static void CountingSort(ReadOnlySpan<Location> source, int n, int place) // function to implement counting sort  
        //{
        //    var output = source;

        //    var bytes = source[0].CityAsSbytes;
        //    foreach (var item in bytes)
        //    {
                
        //    }

        //    int[] count = new int[24];

        //    // Calculate count of elements  
        //    for (int i = 0; i < n; i++)
        //        count[(output[i] / place) % 10]++;

        //    // Calculate cumulative frequency  
        //    for (int i = 1; i < 10; i++)
        //        count[i] += count[i - 1];

        //    // Place the elements in sorted order  
        //    for (int i = n - 1; i >= 0; i--)
        //    {
        //        output[count[(a[i] / place) % 10] - 1] = a[i];
        //        count[(a[i] / place) % 10]--;
        //    }

        //    for (int i = 0; i < n; i++)
        //        a[i] = output[i];
        //}

        // function to implement radix sort  
        public static void Sort(in ReadOnlySpan<Location> source, int length)
        {
            for (int place = length; place >= 0; place--)
            {
                //CountingSort(source, length, place);
            }
        }
    }
}
