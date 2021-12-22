using Core.Interfaces;

namespace Infrastructure.Repository.DbSet
{
    public abstract class BinaryDbSet
    {
        protected IByteReader Reader { get; }
        protected int Offset { get; }
        protected int ArrayLength { get; }
        protected BinaryDbSet(in IByteReader reader)
        {
            Reader = reader;
        }
        protected BinaryDbSet(in IByteReader reader, int offset, int arrayLength) 
        {
            Reader = reader;
            Offset = offset;
            ArrayLength = arrayLength;
        }
    }
}
