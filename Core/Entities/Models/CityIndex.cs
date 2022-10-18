using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Core.Entities.Models.Columns;

namespace Core.Entities.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly struct CityIndex : IIndex
    {
        [field: FieldOffset(0)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public uint Offset { get; }
    }
}
