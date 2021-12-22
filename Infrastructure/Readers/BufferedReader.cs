using System;
using System.IO;

namespace Infrastructure.Readers
{
	public class BufferedReader : IDisposable
	{
		public Stream BaseStream { get; }
		private readonly byte[] _buffer;
		private readonly int _bufferSize;
		private readonly int _headerSize;
		private int _offset;
		private int _bufferedBytes;
        public BufferedReader(MemoryStream stream, int headerSize = 60, int bufferSize = 9600)
		{
			BaseStream = stream;
			_bufferSize = bufferSize;
			_headerSize = headerSize;
			_buffer = new byte[_bufferSize];
			_offset = 0;
			_bufferedBytes = 0;
		}

		public int BytesAvailable => Math.Max(0, _bufferedBytes - _offset);
		public bool FillBuffer()
		{
			var bytesUnread = _bufferSize - _offset;
			var bytesToRead = _bufferSize - bytesUnread;
			_bufferedBytes = bytesUnread;
			_offset = 0;
			if (bytesUnread > 0) 
			{
				Buffer.BlockCopy(_buffer, bytesToRead, _buffer, 0, bytesUnread);
			}
			while (bytesToRead > 0)
			{
				var bytesRead = BaseStream.Read(_buffer, bytesUnread, bytesToRead);
				if (bytesRead == 0) 
				{
					return false;
				}
				_bufferedBytes += bytesRead;
				bytesToRead -= bytesRead;
				bytesUnread += bytesRead;
			}
			return true;
		}
		public void FillHeader()
		{
			var bytesUnread = 0;
			do
			{
				var bytesRead = BaseStream.Read(_buffer, 0, _bufferSize - bytesUnread);
				if (bytesRead <= 0)
				{
					return;
				}
				bytesUnread += bytesRead;
			} 
			while (bytesUnread < _headerSize);	
		}

		public byte ReadByte() => _buffer[_offset++];
		public sbyte[] ReadSBytes(int count)
		{
			sbyte[] sbytes = new sbyte[count];
			Array.Copy(_buffer[_offset..(_offset + count)], sbytes, count);
			return sbytes;
		}
		public ushort ReadUInt16()
		{
			var val = (_buffer[_offset] | _buffer[_offset + 1] << 8);
			_offset += 2;
			return (ushort)val;
		}


		[System.Security.SecuritySafeCritical]
		public unsafe float ReadSingle()
		{
            var val = (_buffer[_offset] | _buffer[_offset + 1] << 8 | _buffer[_offset + 2] << 16 | _buffer[_offset + 3] << 24);
            _offset += 4;
            return *((float*)&val);
        }
		public int ReadInt32()
		{
			var val = (_buffer[_offset] | _buffer[_offset + 1] << 8 | _buffer[_offset + 2] << 16 | _buffer[_offset + 3] << 24);
			_offset += 4;
			return val;
		}
		public uint ReadUInt32()
		{
			var val = (_buffer[_offset] | _buffer[_offset + 1] << 8 | _buffer[_offset + 2] << 16 | _buffer[_offset + 3] << 24);
			_offset += 4;
			return (uint)val;
		}
		public ulong ReadUInt64()
		{
			uint lo = (uint)(_buffer[_offset] | _buffer[_offset + 1] << 8 | _buffer[_offset + 2] << 16 | _buffer[_offset + 3] << 24);
			uint hi = (uint)(_buffer[_offset + 4] | _buffer[_offset + 5] << 8 | _buffer[_offset + 6] << 16 | _buffer[_offset + 7] << 24);
			_offset += 8;
			return ((ulong)hi) << 32 | lo;
		}				
        public byte[] ReadBytes(int count)
        {
            Span<byte> bytes = _buffer.AsSpan(_offset, count);
            _offset += count;
            return bytes.ToArray();
        }
        public unsafe char[] ReadChars(int count)
        {
			Span<char> chars =stackalloc char[count];
			fixed (byte* buffer = _buffer)
            {
                for (int i = 0; i < count; i++)
                {
					chars[i] = (char)buffer[_offset++];
                }
            }
            return chars.ToArray();
        }
        public void Dispose()
		{
			BaseStream.Close();
		}
	}
}
