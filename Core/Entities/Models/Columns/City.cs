using Core.Converters;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Buffers;
using System.Buffers.Binary;
using Core.Exceptions;

namespace Core.Entities.Models.Columns
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public readonly struct City : IComparable<City>, IColumn
    {
        #region Fields
        [FieldOffset(0)] private readonly sbyte a;
        [FieldOffset(1)] private readonly sbyte b;
        [FieldOffset(2)] private readonly sbyte c;
        [FieldOffset(3)] private readonly sbyte d;
        [FieldOffset(4)] private readonly sbyte e;
        [FieldOffset(5)] private readonly sbyte f;
        [FieldOffset(6)] private readonly sbyte g;
        [FieldOffset(7)] private readonly sbyte h;
        [FieldOffset(8)] private readonly sbyte i;
        [FieldOffset(9)] private readonly sbyte j;
        [FieldOffset(10)] private readonly sbyte k;
        [FieldOffset(11)] private readonly sbyte l;
        [FieldOffset(12)] private readonly sbyte m;
        [FieldOffset(13)] private readonly sbyte n;
        [FieldOffset(14)] private readonly sbyte o;
        [FieldOffset(15)] private readonly sbyte p;
        [FieldOffset(16)] private readonly sbyte r;
        [FieldOffset(17)] private readonly sbyte s;
        [FieldOffset(18)] private readonly sbyte t;
        [FieldOffset(19)] private readonly sbyte q;
        [FieldOffset(20)] private readonly sbyte u;
        [FieldOffset(21)] private readonly sbyte v;
        [FieldOffset(22)] private readonly sbyte w;
        [FieldOffset(23)] private readonly sbyte x;
        #endregion
        //public unsafe int CompareTo(City match)
        //{     
        //    var matchedPtr = Marshal.AllocHGlobal(sizeof(City));
        //    var thisPtr = Marshal.AllocHGlobal(sizeof(City));
        //    try
        //    {
        //        Marshal.StructureToPtr(this, thisPtr, true);
        //        Marshal.StructureToPtr(match, matchedPtr, true);
        //        return CompareSBytes((sbyte*)thisPtr, (sbyte*)matchedPtr);
        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(thisPtr);
        //        Marshal.FreeHGlobal(matchedPtr);
        //    }
        //}

        public unsafe int CompareTo(City city) //for arrays sorted by MSD
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


        //public unsafe int CompareTo(City match) //for arrays sorted by LSD
        //{
        //    fixed (City* city = &this)
        //    {
        //        const int offset = 8;
        //        var target = (sbyte*)&match;
        //        var origin = (sbyte*)city;
        //        //return Compare((sbyte*)origin, (sbyte*)target);

        //        if (*(ulong*)origin == *(ulong*)target &&
        //            *(ulong*)(origin + offset) == *(ulong*)(target + offset) &&
        //            *(ulong*)(origin + (offset + offset)) == *(ulong*)(target + (offset + offset)))
        //        {
        //            return 0;
        //        }

        //        if (*(ulong*)origin < *(ulong*)target)
        //        {
        //            return -1;
        //        }
        //        if (*(ulong*)origin > *(ulong*)target)
        //        {
        //            return 1;
        //        }

        //        if (*(ulong*)(origin + offset) < *(ulong*)(target + offset))
        //        {
        //            return -1;
        //        }
        //        if (*(ulong*)(origin + offset) > *(ulong*)(target + offset))
        //        {
        //            return 1;
        //        }

        //        if (*(ulong*)(origin + (offset + offset)) < *(ulong*)(target + (offset + offset)))
        //        {
        //            return -1;
        //        }
        //        if (*(ulong*)(origin + (offset + offset)) > *(ulong*)(target + (offset + offset)))
        //        {
        //            return 1;
        //        }
        //        throw new Exception();
        //    }
        //}

        //private static unsafe int Compare(sbyte* origin, sbyte* target)
        //{
        //    if (*(ulong*)origin == *(ulong*)target && *(ulong*)(origin + 8) == *(ulong*)(target + 8) && *(ulong*)(origin + 16) == *(ulong*)(target + 16)) //Что бы не проверять все
        //    {
        //        return 0;
        //    }
        //    if (*(ulong*)origin < *(ulong*)target)
        //    {
        //        return -1;
        //    }
        //    if (*(ulong*)origin > *(ulong*)target)
        //    {
        //        return 1;
        //    }
        //    if (*(ulong*)(origin + 8) < *(ulong*)(target + 8))
        //    {
        //        return -1;
        //    }
        //    if (*(ulong*)(origin + 8) > *(ulong*)(target + 8))
        //    {
        //        return 1;
        //    }
        //    if (*(ulong*)(origin + 16) < *(ulong*)(target + 16))
        //    {
        //        return -1;
        //    }
        //    if (*(ulong*)(origin + 16) > *(ulong*)(target + 16))
        //    {
        //        return 1;
        //    }
        //    throw new Exception();
        //}
    }
}


