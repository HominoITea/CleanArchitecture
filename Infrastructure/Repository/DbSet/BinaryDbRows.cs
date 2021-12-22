using Core.Interfaces;
using System.Linq;

namespace Infrastructure.Repository.DbSet
{
    public class BinaryDbRows<TEntity> : BinaryDbSet where TEntity : IEntity
    {
        public TEntity[] Rows { get; set; }
        public BinaryDbRows(in IByteReader reader, int offset, int arrayLength) : base(in reader, offset, arrayLength)
        {
            Rows = Reader.BytesToStructureArray<TEntity>(Offset, ArrayLength);
        }
        public IQueryable<TEntity> AsQueryable() => Rows.AsQueryable();
        public TEntity this[int index] => Rows[index];
    }
}
