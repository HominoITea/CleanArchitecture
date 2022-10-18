using BenchmarkDotNet.Attributes;
using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Runtime.InteropServices;


namespace Infrastructure.Readers
{
    public class FileByteReader : IByteReader
    {
        private IBuffer _buffer;
        private IntPtr _pointer;
        private GCHandle _handler;

        public IByteReader Set(IBuffer buffer)
        {
            _buffer = buffer;
            _handler = GCHandle.Alloc(_buffer.Get(), GCHandleType.Pinned);
            _pointer = _handler.AddrOfPinnedObject();
            return this;
        }

        [Benchmark]
        public Span<T> BufferToStructArray<T>(int offset, int rows) where T : struct, IEntity
        {
            Marshal.Copy(_buffer.Get(), offset, _pointer, Marshal.SizeOf(typeof(T)) * rows);
            unsafe
            {
                return new Span<T>(_pointer.ToPointer(), rows);
            }
        }

        [Benchmark]
        public T HeaderToStruct<T>() where T : struct, IHeader
        {
            return (T)(Marshal.PtrToStructure(_pointer, typeof(T)) ?? throw new NullPointerException());
        }

        public void Dispose()
        {
            _handler.Free();
            _buffer = null;
        }
    }
}
