using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Core.Entities.Models.Columns;

namespace Core.Entities.Models
{
    public readonly struct CityIndex : IEntity
    {
        public uint Offset { get; }
    }
}
