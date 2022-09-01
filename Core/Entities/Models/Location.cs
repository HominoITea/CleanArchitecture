using Core.Converters;
using Core.Entities.Models.Columns;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 96)]
    public readonly struct Location : IEntity, IComparable<City>
    {
        [FieldOffset(0)]
        private readonly Country _country; // название страны (случайная строка с префиксом "cou_")  [8]; 

        [FieldOffset(8)]
        private readonly Region _region; // название области (случайная строка с префиксом "reg_") [12];

        [FieldOffset(20)]
        private readonly Postal _postal; // почтовый индекс (случайная строка с префиксом "pos_") [12]; 

        [FieldOffset(32)] private readonly City _city; // название города (случайная строка с префиксом "cit_") [24]; 

        [FieldOffset(56)]
        private readonly Organization _organization; // название организации (случайная строка с префиксом "org_") [32];

        [FieldOffset(88)] private readonly float _latitude; // широта
        [FieldOffset(92)] private readonly float _longitude; // долгота

        public char[] CityAsChars => Map.ObjectToCharArray(_city);
        public sbyte[] CityAsSbytes => Map.ObjectToSbyteArray(_city);
        public int CompareTo(City cityName) => _city.CompareTo(cityName);

        public static int SizeOf => Marshal.SizeOf(typeof(Location));

    }
}
