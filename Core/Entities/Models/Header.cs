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
        private readonly int _version;         // версия база данных
        [FieldOffset(4)]
        private readonly Name _name;        // название/префикс для базы данных [32]; 
        [FieldOffset(36)]
        private readonly ulong _timeStamp;          // время создания базы данных

        [FieldOffset(48)]
        private readonly uint _offsetRanges;      // смещение относительно начала файла до начала списка записей с геоинформацией
        [FieldOffset(52)]
        private readonly uint _offsetCities;    // смещение относительно начала файла до начала индекса с сортировкой по названию городов
        [FieldOffset(56)]
        private readonly uint _offsetLocations;   // смещение относительно начала файла до начала списка записей о местоположении

        [field: FieldOffset(44)]
        public int Records { get; }

        public int LocationOffset => (int)_offsetLocations;
        public int IpRangeOffset  => (int)_offsetRanges;
        public int CityIndexOffset => (int)_offsetCities;
    }
}
