using Core.Entities.Models;
using System;
using System.Collections;

namespace Infrastructure.Services
{
    public static class BinarySearcher
    {
        public static (int, int) BinarySearch<TKey, TEntity>(in ReadOnlySpan<CityIndex> indices, in ReadOnlySpan<TEntity> source, int size, TKey key) 
            where TEntity : IComparable<TKey>
        {
            var lowerIndex = BoundSearch(indices, source, size, key, 
                (compareTo, target) => compareTo < target ? 0 : 1);
            var upperIndex = BoundSearch(indices, source, size, key, 
                (compareTo, target) => !(compareTo > target) ? 0 : 1);
            return (lowerIndex, --upperIndex);
        }

        private static int BoundSearch<TKey, TEntity>(ReadOnlySpan<CityIndex> indices, ReadOnlySpan<TEntity> source, int size, TKey key, 
            Comparison<int> compareDelegate, int firstElement = 0) 
            where TEntity : IComparable<TKey>
        {
            var left = firstElement;
            var distance = indices.Length;
            while (distance > 0)
            {
                var middle = distance / 2;
                var current = left + middle;
                var compared = source[(int)indices[current].Offset / size].CompareTo(key);
                if (compareDelegate(compared, 0) == 0)
                {
                    left = ++current;
                    distance -= ++middle;
                }
                else
                {
                    distance = middle;
                }
            }
            return left;
        }
    }
}
