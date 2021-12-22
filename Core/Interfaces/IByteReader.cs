using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IByteReader
    {
        public byte[] FillBuffer(int offset, int length);
        public T[] BytesToStructureArray<T>(int offset, int arrayLength) where T : IEntity;
        public T BytesToStructure<T>(int offset) where T : struct, IEntity;
    }
}
