﻿using Core.Converters;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Buffers;
using System.Buffers.Binary;

namespace Core.Entities.Models.Columns
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public readonly struct City : IComparable<City>, IColumn
    {
        #region Fields
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
        [FieldOffset(8)]
        public readonly sbyte i;
        [FieldOffset(9)]
        public readonly sbyte j;
        [FieldOffset(10)]
        public readonly sbyte k;
        [FieldOffset(11)]
        public readonly sbyte l;
        [FieldOffset(12)]
        public readonly sbyte m;
        [FieldOffset(13)]
        public readonly sbyte n;
        [FieldOffset(14)]
        public readonly sbyte o;
        [FieldOffset(15)]
        public readonly sbyte p;
        [FieldOffset(16)]
        public readonly sbyte r;
        [FieldOffset(17)]
        public readonly sbyte s;
        [FieldOffset(18)]
        public readonly sbyte t;
        [FieldOffset(19)]
        public readonly sbyte q;
        [FieldOffset(20)]
        public readonly sbyte u;
        [FieldOffset(21)]
        public readonly sbyte v;
        [FieldOffset(22)]
        public readonly sbyte w;
        [FieldOffset(23)]
        public readonly sbyte x;
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
        public unsafe int CompareTo(City match)
        {            
            fixed (City* origin = &this)
            {
                City* target = &match;
                return CompareSbytes((sbyte*)origin, (sbyte*)target);
            }
        }
        public unsafe int CompareSbytes(sbyte* origin, sbyte* target)
        {
            if (*(ulong*)origin == *(ulong*)target && *(decimal*)(origin + 8) == *(decimal*)(target + 8)) //Что бы не проверять все
            {
                return 0;
            }
            if (*(ulong*)origin < *(ulong*)target)
            {
                return -1;
            }
            if (*(ulong*)origin > *(ulong*)target)
            {
                return 1;
            }
            if (*(decimal*)(origin + 8) < *(decimal*)(target + 8))
            {
                return -1;
            }
            if (*(decimal*)(origin + 8) > *(decimal*)(target + 8))
            {
                return 1;
            }
            throw new Exception();
        }
    }
}


