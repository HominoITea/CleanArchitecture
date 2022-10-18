using Core.Interfaces;
using System;
using System.Linq;

namespace Infrastructure.Middleware.DbSet
{
    public class BinaryDbRows<TEntity> : BinaryDbSet where TEntity : struct, IEntity
    {
        private readonly TEntity[] _data;

        public BinaryDbRows(in IByteReader reader, int offset, int rows) : base(in reader, offset, rows) => 
            _data = Reader.BufferToStructArray<TEntity>(offset, rows).ToArray();

        public ReadOnlySpan<TEntity> AsReadOnlySpan() => new ReadOnlySpan<TEntity>(_data);

        public TEntity this[int index] => _data[index];

        public ReadOnlySpan<TEntity> AsSorted<TKey>(Func<TEntity, TKey> sortingFunc) => _data.OrderBy(sortingFunc).ToArray();
    }
}
