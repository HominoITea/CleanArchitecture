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
        private readonly sbyte _a;
        [FieldOffset(1)]
        private readonly sbyte _b;
        [FieldOffset(2)]
        private readonly sbyte _c;
        [FieldOffset(3)]
        private readonly sbyte _d;
        [FieldOffset(4)]
        private readonly sbyte _e;
        [FieldOffset(5)]
        private readonly sbyte _f;
        [FieldOffset(6)]
        private readonly sbyte _g;
        [FieldOffset(7)]
        private readonly sbyte _h;
    }
}
