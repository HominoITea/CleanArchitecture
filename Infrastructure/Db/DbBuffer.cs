using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;

namespace Infrastructure.Db
{
    public class DbBuffer : IBuffer
    {
        private byte[] _data;
        public byte[] Get() => _data;
        public IBuffer Set(in byte[] data)
        {
            _data = data;
            return this;
        }
    }
}
