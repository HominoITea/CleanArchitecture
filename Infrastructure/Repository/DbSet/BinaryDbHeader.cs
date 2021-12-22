using Core.Interfaces;

namespace Infrastructure.Repository.DbSet
{
    public class BinaryDbHeader<THeader> : BinaryDbSet where THeader : struct, IEntity
    {
        public THeader Data { get; }
        public BinaryDbHeader(in IByteReader reader) : base(in reader)
        {
            Data = Reader.BytesToStructure<THeader>(Offset);
        }
    }
}