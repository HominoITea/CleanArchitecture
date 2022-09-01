using System;
using System.Collections.Generic;
using System.Runtime;
using Core.Interfaces;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Entities.Models;
using Core.Exceptions;
using Infrastructure.Db;
using Microsoft.Extensions.Options;


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

        //public T[] BufferToStructArray<T>(int offset, int arrayLength) where T : IEntity
        //{
        //    var structures = new T[arrayLength];
        //    var buffer = GetBuffer(offset, SizeOf<T>() * arrayLength);
        //    var handle = GCHandle.Alloc(structures, GCHandleType.Pinned);
        //    try
        //    {
        //        Marshal.Copy(buffer, 0, handle.AddrOfPinnedObject(), buffer.Length);
        //        return structures;
        //    }
        //    finally
        //    {
        //        handle.Free();
        //    }
        //}

        public Span<T> BufferToStructArray<T>(int offset, int rows) where T : struct, IEntity
        {
            Marshal.Copy(_buffer.Get(), offset, _pointer, Marshal.SizeOf(typeof(T)) * rows);
            unsafe
            {
                return new Span<T>(_pointer.ToPointer(), rows);
            }
        }

        public T HeaderToStruct<T>() where T : struct, IHeader
        {
            return (T)(Marshal.PtrToStructure(_pointer, typeof(T)) ?? throw new NullPointerException());
        }

        // private static int SizeOf<T>() => Marshal.SizeOf(typeof(T));
        //private byte[] GetBuffer(int offset, int length) => _buffer[offset..(offset + length)];
        public void Dispose()
        {
            _handler.Free();
            _buffer = null;
        }
    }
}
