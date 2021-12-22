using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Infrastructure.Readers
{
    public ref struct ByteBuffer
    {
        public readonly byte[] _buffer;

        public ByteBuffer(byte[] buffer)
        {
            _buffer = buffer;
        }
    }

}
