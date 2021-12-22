using Core.Entities.Models.Columns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Services
{
    class BinarySearcher
    {

        //public static (bool, int) BinarySearch<T>(Span<T> source, char[] value, bool descending = false) where T : IComparable<char[]>
        //{
        //    int left = 0, middle;
        //    for (int right = source.Length; left < right;)
        //    {
        //        if (source[left].CompareTo(value) == 0)
        //        {
        //            return (true, left);
        //        }
        //        middle = left + (right - left) / 2;
        //        var c = source[middle].CompareTo(value);
        //        if (c == 0)
        //        {
        //            if (middle == left + 1)
        //            {
        //                return (true, middle);
        //            }
        //            right = middle + 1;
        //        }
        //        if ((c < 0) == descending)
        //        {
        //            right = middle;
        //        }
        //        else
        //        {
        //            left = middle + 1;
        //        }
        //    }
        //    return (false, left);
        //}


        //public static (bool, int) BinarySearch<T>(in IReadOnlyCollection<T> source, T value, bool descending = false) where T : IComparable<T>
        //int GetRange(int range, int value) 
        //{
        //    if (source[left].CompareTo(value) == 0)
        //    {
        //        return range;
        //    }
        //    return range;
        //}

        public static (int left, int middle, int rigth) BinarySearch<TEntity, TKey>(in ReadOnlySpan<TEntity> source, TKey value, bool isUpper = false) where TEntity : IComparable<TKey>
        {
             
            var lastIndex = source.Length - 1;
            var range = (left: 0, middle: lastIndex / 2, right: lastIndex);
            if (!isUpper)
            {
                if (source[range.left].CompareTo(value) == 0)
                {
                    return range;
                }
            }
            else
            {
                if (source[range.right].CompareTo(value) == 0)
                {
                    return range;
                }
            }
            while (range.left < range.right)
            {
                var compare = source[range.middle].CompareTo(value);
                if (compare == 0)
                {
                    if (!isUpper)
                    {
                        if (range.middle == range.left + 1)
                        {
                            return range;
                        }
                        range.right = range.middle + 1;
                    }
                    else
                    {
                        if (range.middle == range.right - 1)
                        {
                            return range;
                        }
                        range.left = range.middle - 1;                        
                    }
                }
                if (!isUpper)
                {
                    if ((compare < 0) == isUpper)
                    {
                        range.right = range.middle;
                    }
                    else
                    {
                        range.left = range.middle + 1;
                    }
                }
                else
                {
                    if ((compare > 0) == isUpper)
                    {
                        range.right = range.middle - 1;
                    }
                    else
                    {
                        range.left = range.middle;
                    }
                }
                range.middle = range.left + (range.right - range.left) / 2;
            }
            Predicate<bool> predicate = (isUpper) =>
            {
                return isUpper;
            };
            return range;
            
        }

        public static int LowerBoundSearch<TEntity, TKey>(in ReadOnlySpan<TEntity> source, TKey value) where TEntity : IComparable<TKey>
        {
            int middle;
            int compare;
            var left = 0;
            var right = source.Length - 1;
            if (source[left].CompareTo(value) == 0)
            {
                return left;
            }
            while (left < right)
            {                
                middle = left + (right - left) / 2;
                compare = source[middle].CompareTo(value);
                if (compare == 0)
                {
                    if (middle == left + 1)
                    {
                        return  middle;
                    }
                    right = middle + 1;
                }
                if ((compare < 0) == false)
                {
                    right = middle;
                }
                else
                {
                    left = middle +  1;
                }
            }

            return right;
        }

        public static int UpperBoundSearch<TEntity, TKey>(in ReadOnlySpan<TEntity> source, TKey value) where TEntity : IComparable<TKey>
        {
            int middle;
            int compare;
            var left = 0;
            var right = source.Length - 1;
            if (source[right].CompareTo(value) == 0)
            {
                return right;
            }
            while (left < right)
            {                
                middle = right - (right - left) / 2;
                compare = source[middle].CompareTo(value);
                if (compare == 0)
                {
                    if (middle == right - 1)
                    {
                        return middle;
                    }
                    left = middle - 1;
                }
                if ((compare > 0) == true)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle;
                }
            }
            return left;
        }
    }
}
