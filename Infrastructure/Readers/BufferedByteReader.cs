using Core.Interfaces;
using System.Runtime.InteropServices;


namespace Infrastructure.Readers
{
    public readonly struct BufferedByteReader : IByteReader
    {
        private readonly byte[] _buffer;        
        public BufferedByteReader(in byte[] buffer)
        {
            _buffer = buffer; 
        }        
        public T[] BytesToStructureArray<T>(int offset, int arrayLength) where T: IEntity
        {            
            var structures = new T[arrayLength];
            var buffer = FillBuffer(offset, GetTypeSize<T>() * arrayLength);
            var handle = GCHandle.Alloc(structures, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(buffer, 0, handle.AddrOfPinnedObject(), buffer.Length);
                return structures;
            }
            finally
            {
                handle.Free();
            }
        }
        public T BytesToStructure<T>(int offset) where T : struct, IEntity
        {
            var buffer = FillBuffer(offset, GetTypeSize<T>());
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var structure = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                return structure;
            }
            finally
            {                
                handle.Free();
            }
        }
        private static int GetTypeSize<T>() => Marshal.SizeOf(typeof(T));
        public byte[] FillBuffer(int offset, int length) => _buffer[offset..(offset + length)];        
    }
}
