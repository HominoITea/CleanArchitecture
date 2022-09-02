using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IIndex : IEntity
    {
        public uint Offset { get; }
    }
}
