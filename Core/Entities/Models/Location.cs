using Core.Entities.Models.Columns;
using Core.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Core.Entities.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 96)]
    public readonly struct Location : IEntity, IComparable<City>
    {
        [FieldOffset(0)]
        private readonly Country _country; 

        [FieldOffset(8)]
        private readonly Region _region; 

        [FieldOffset(20)]
        private readonly Postal _postal; 

        [field: FieldOffset(32)] // ReSharper disable once UnassignedGetOnlyAutoProperty
        private City City { get; }  

        [FieldOffset(56)]
        private readonly Organization _organization; 

        [FieldOffset(88)] 
        private readonly float _latitude; 

        [FieldOffset(92)] 
        private readonly float _longitude; 

        public int CompareTo(City cityName) => City.CompareTo(cityName);

        public static int SizeOf => Marshal.SizeOf(typeof(Location));

    }
}
