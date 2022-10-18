using Core.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Core.Entities.Models.Columns
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public readonly struct City : IComparable<City>, IColumn
    {
        #region Fields
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
        [FieldOffset(8)]
        private readonly sbyte _i;
        [FieldOffset(9)]
        private readonly sbyte _j;
        [FieldOffset(10)]
        private readonly sbyte _k;
        [FieldOffset(11)]
        private readonly sbyte _l;
        [FieldOffset(12)]
        private readonly sbyte _m;
        [FieldOffset(13)]
        private readonly sbyte _n;
        [FieldOffset(14)]
        private readonly sbyte _o;
        [FieldOffset(15)]
        private readonly sbyte _p;
        [FieldOffset(16)]
        private readonly sbyte _r;
        [FieldOffset(17)]
        private readonly sbyte _s;
        [FieldOffset(18)]
        private readonly sbyte _t;
        [FieldOffset(19)]
        private readonly sbyte _q;
        [FieldOffset(20)]
        private readonly sbyte _u;
        [FieldOffset(21)]
        private readonly sbyte _v;
        [FieldOffset(22)]
        private readonly sbyte _w;
        [FieldOffset(23)]
        private readonly sbyte _x;
        #endregion

        public unsafe int CompareTo(City city)
        {
            #region constants and vars
            const int nullCode = 0;
            const int spaceCode = 32;
            const int left = 1;
            const int right = -1;
            const int matched = 0;
            const int next = 1;

            var offset = 0;
            #endregion
            fixed (City* current = &this)
            {
                var origin = (sbyte*)current;
                var target = (sbyte*)&city;
                while (offset < sizeof(City))
                {
                    if (*(origin + offset) == spaceCode && *(origin + (offset + next)) == nullCode && *(target + offset) == nullCode) // In case a search query string was trimmed. If there is a space at the end of the city name (as stored in the db) no need to use.
                    {
                        return matched;
                    }
                    if (*(origin + offset) != *(target + offset))
                    {
                        return (*(origin + offset) > *(target + offset)) ? left : right;
                    }
                    if (*(origin + offset) == nullCode && *(target + offset) == nullCode)
                    {
                        break;
                    }
                    offset += next;
                }
                return matched;
            }
        }
    }
}


