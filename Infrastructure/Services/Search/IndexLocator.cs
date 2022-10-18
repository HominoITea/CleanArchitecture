using System;
using BenchmarkDotNet.Attributes;
using Core.Exceptions;
using Core.Interfaces;

namespace Infrastructure.Services.Search
{
    public static class IndexLocator
    {
        public static (int FirstElement, int LastElement) RangeSearch<TKey, TEntity, TIndex>(in SearchComponents<TKey, TEntity, TIndex> components, TKey key)
            where TEntity : IComparable<TKey> 
            where TIndex : IIndex
        {
            var lowerIndex = BoundSearch(components, key, (compareTo, target) => compareTo < target ? 0 : 1);
            var upperIndex = BoundSearch(components, key, (compareTo, target) => !(compareTo > target) ? 0 : 1,
                lowerIndex);

            if (lowerIndex >= upperIndex && lowerIndex < components.Indices.Length - 1) //these values can't be equal or lower greater than upper, cause upper always return next index. Only if lower is max value in array, it will be same
            {
                throw new EntityNotFoundException();
            }

            return lowerIndex == components.Indices.Length - 1 ? (lowerIndex, upperIndex) : (lowerIndex, --upperIndex);
        }
        [Benchmark]
        private static int BoundSearch<TKey, TEntity, TIndex>(in SearchComponents<TKey, TEntity, TIndex> components, TKey key, Comparison<int> compareDelegate, int startIndex = 0) 
            where TEntity : IComparable<TKey> 
            where TIndex : IIndex
        {
            var left = startIndex;
            var distance = components.Indices.Length - left;
            while (distance > 0)
            {
                var middle = distance >> 1;
                var current = left + middle;
                var index = (int)components.Indices[current].Offset / components.EntitySize;
                if (compareDelegate(components.Source[index].CompareTo(key), 0) == 0)
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
