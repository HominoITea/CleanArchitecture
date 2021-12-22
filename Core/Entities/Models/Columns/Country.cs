using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Entities.Models.Columns
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]

    public readonly struct Country
    {
        [FieldOffset(0)]
        public readonly sbyte a;
        [FieldOffset(1)]
        public readonly sbyte b;
        [FieldOffset(2)]
        public readonly sbyte c;
        [FieldOffset(3)]
        public readonly sbyte d;
        [FieldOffset(4)]
        public readonly sbyte e;
        [FieldOffset(5)]
        public readonly sbyte f;
        [FieldOffset(6)]
        public readonly sbyte g;
        [FieldOffset(7)]
        public readonly sbyte h;
    }
}
