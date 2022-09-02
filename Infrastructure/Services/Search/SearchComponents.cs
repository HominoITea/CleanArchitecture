using System;
using Core.Interfaces;

namespace Infrastructure.Services.Search
{
    public readonly ref struct SearchComponents<TKey, TEntity, TIndex>
        where TEntity : IComparable<TKey>
        where TIndex : IIndex
    {
        internal ReadOnlySpan<TIndex> Indices { get; }
        internal ReadOnlySpan<TEntity> Source { get; }
        internal int EntitySize { get; }

        public SearchComponents(in ReadOnlySpan<TIndex> indices, in ReadOnlySpan<TEntity> source, int entitySize)
        {
            Indices = indices;
            Source = source;
            EntitySize = entitySize;
        }
    }
}
