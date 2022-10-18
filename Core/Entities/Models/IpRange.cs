using Core.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Core.Entities.Models
{

    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public readonly struct IpRange : IEntity, IComparable<uint>
    {
        [field: FieldOffset(0)]// ReSharper disable once UnassignedGetOnlyAutoProperty
        public uint IpFrom { get; } 

        [field: FieldOffset(4)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public uint IpTo { get; }

        [field: FieldOffset(8)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public uint LocationIndex { get; }

        public int CompareTo(uint ip)
        {
            if (ip > IpTo) 
                return 1;
            if (ip < IpFrom) 
                return -1;
            return 0;
        }
    }
}
