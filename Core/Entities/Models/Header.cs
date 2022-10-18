using Core.Entities.Models.Columns;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Core.Interfaces;

namespace Core.Entities.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 60)]
    public readonly struct Header : IHeader
    {
        [FieldOffset(0)]
        private readonly int _version;         
        [FieldOffset(4)]
        private readonly Name _name;        
        [FieldOffset(36)]
        private readonly ulong _timeStamp;

        [field: FieldOffset(44)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public int Records { get; }

        [field: FieldOffset(48)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public int IpRangeOffset { get; }

        [field: FieldOffset(52)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public int CityIndexOffset { get; }

        [field: FieldOffset(56)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        public int LocationOffset { get; }
    }
}
