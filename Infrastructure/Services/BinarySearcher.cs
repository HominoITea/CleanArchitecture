using Core.Entities.Models;
using System;
using System.Collections;

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

        public static int BinarySearch<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices, in ReadOnlySpan<TEntity> source, 
            int elementSize, TKey value, bool isUpper, int firstIndex = 0) where TEntity : IComparable<TKey>
        {
            var middle = 0;
            var compare = 0;
            var left = firstIndex;
            var right = indices.Length - 1;
            while (left < right)
            {
                middle = left + (right - left) / 2;
                compare = source[(int)indices[middle].Offset / elementSize].CompareTo(value);

                if (compare == 0)
                {
                    if (isUpper)
                    {
                        left = (middle == right) ? --middle : middle;
                    }
                    else
                    {
                        right = (middle == ++left) ? --middle : ++middle;
                    }
                }
                else
                {
                    if (compare > 0)
                    {
                        right = isUpper ? --middle : middle;
                    }
                    else
                    {
                        left = isUpper ? --middle : ++middle;
                    }
                }
            }
            return isUpper ? left : right;
        }

        public static int BinarySearch2<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices, in ReadOnlySpan<TEntity> source,
            int elementSize, TKey value, bool isUpper, int firstIndex = 0) where TEntity : IComparable<TKey>
        {
            var middle = 0;
            var compare = 0;
            var left = firstIndex;
            var right = indices.Length - 1;
            while (left < right)
            {
                middle = left + (right - left) / 2;
                compare = source[(int)indices[middle].Offset / elementSize].CompareTo(value);

                if (compare == 0)
                {
                    if (!isUpper)
                    {
                        left = (middle == right) ? --middle : middle;
                    }
                    else
                    {
                        right = (middle == ++left) ? middle : ++middle;
                    }
                }
                else
                {
                    if (compare > 0)
                    {
                        right = middle;
                    }
                    else
                    {
                        left = ++middle;
                    }
                }
            }
            return isUpper ? left : right;
        }

        public static void LowerBound<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices,
            in ReadOnlySpan<TEntity> source, int elementSize, TKey value, Comparer comparer) where TEntity : IComparable<TKey>
        {

        }

        public static int LowerBoundSearch<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices,
            in ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        {
            var middle = 0;
            var compare = 0;
            var left = 0;
            var right = indices.Length - 1;
            while (left < right)
            {
                middle = left + (right - left) / 2;
                compare = source[(int)indices[middle].Offset / elementSize].CompareTo(value);

                if (compare >= 0)
                {
                    right = (middle == left) ? middle : --middle;
                }
                else
                { 
                    left = ++middle;
                }
            }
            return right;
        }



        public static int UpperBoundSearch<TKey, TEntity>(ReadOnlySpan<CityIndex> indices, ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        {
            var left = 0;
            var middle = 0;
            var compare = 0;
            var right = indices.Length - 1;
            while (left < right)
            {
                middle = right - (right - left) / 2;
                compare = source[(int)indices[middle].Offset / elementSize].CompareTo(value);

                if (compare <= 0)
                {
                    left = (middle == right) ? middle : ++middle ;
                }
                else
                {
                    right = --middle;
                }
            }
            return left;
        }

        public static int LowerBoundSearch2<TKey, TEntity>(ReadOnlySpan<CityIndex> indices, ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        {
            var first = 0;
            var distance = indices.Length;
            while (distance > 0)
            {
                var current = first;
                var middle = distance / 2;
                current += middle;

                if (source[(int)indices[current].Offset / elementSize].CompareTo(value) < 0)
                {
                    first = ++current;
                    distance -= ++middle;
                }
                else
                {
                    distance = middle;
                }
            }
            return first;
        }

        public static int UpperBoundSearch2<TKey, TEntity>(ReadOnlySpan<CityIndex> indices, ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        {
            var first = 0;
            var distance = indices.Length;
            while (distance > 0)
            {
                var current = first;
                var middle = distance / 2;
                current += middle;

                if (!(source[(int)indices[current].Offset / elementSize].CompareTo(value) > 0))
                {
                    first = ++current;
                    distance -= ++middle;
                }
                else
                {
                    distance = middle;
                }
            }
            return first;
        }
        //public static int LowerBoundSearch<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices, in ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        //{
        //    var middle = 0;
        //    var compare = 0;
        //    var left = 0;
        //    var right = indices.Length - 1;
        //    while (left < right)
        //    {
        //        if (source[(int)indices[left].Offset / elementSize].CompareTo(value) == 0)
        //        {
        //            return left;
        //        }
        //        middle = left + (right - left) / 2;
        //        compare = source[(int)indices[middle].Offset / elementSize].CompareTo(value);
        //        if (compare == 0)
        //        {
        //            if (middle == left + 1)
        //            {
        //                return middle;
        //            }
        //            right = middle + 1;
        //        }
        //        else
        //        {
        //            if (!(compare < 0))
        //            {
        //                right = middle;
        //            }
        //            else
        //            {
        //                left = middle + 1;
        //            }
        //        }
        //    }
        //    return right;
        //}

        //public static int UpperBoundSearch<TKey, TEntity>(ReadOnlySpan<CityIndex> indices, ReadOnlySpan<TEntity> source, int elementSize, TKey value) where TEntity : IComparable<TKey>
        //{
        //    var left = 0;
        //    var middle = 0;
        //    var right = indices.Length - 1;
        //    while (left < right)
        //    {
        //        middle = right - (right - left) / 2;
        //        var rightIndex = (int)indices[right].Offset / elementSize;
        //        var middleIndex = (int)indices[middle].Offset / elementSize;
        //        var compare = source[middleIndex].CompareTo(value);

        //        if (source[rightIndex].CompareTo(value) == 0)
        //        {
        //            return right;
        //        }
        //        if (compare == 0)
        //        {
        //            if (middle == right - 1)
        //            {
        //                return middle;
        //            }
        //            left = middle - 1;
        //        }
        //        else
        //        {
        //            if (compare > 0)
        //            {
        //                right = middle - 1;
        //            }
        //            else
        //            {
        //                left = middle;
        //            }
        //        }
        //    }
        //    return left;
        //}
        //private bool IsBoundFound()
        //{
        //    if (source[(int)indices[left].Offset / elementSize].CompareTo(value) == 0)
        //    {
        //        return left;
        //    }
        //} 
    }
}
