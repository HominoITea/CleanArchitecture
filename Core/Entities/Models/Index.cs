using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Entities.Models
{
    public readonly struct Index : IEntity
    {
        public readonly uint Offset { get; }
    }
}
