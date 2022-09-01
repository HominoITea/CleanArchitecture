using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Entities.Models;
using Microsoft.Extensions.Options;

namespace Core.Interfaces
{
    public interface IByteReader: IDisposable
    {
        public IByteReader Set(IBuffer buffer);
        public Span<T> BufferToStructArray<T>(int offset, int rows) where T : struct, IEntity;
        public T HeaderToStruct<T>() where T : struct, IHeader;
    }
}
