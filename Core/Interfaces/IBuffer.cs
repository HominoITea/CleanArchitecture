using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBuffer
    {
        public byte[] Get();
        public IBuffer Set(in byte[] data);
    }
}
