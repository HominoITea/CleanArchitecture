using Core.Entities.Models.Columns;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Core.Interfaces;

namespace Core.Entities.Models
{
    [StructLayout(LayoutKind.Explicit, Size = 60)]
    public readonly struct Header : IEntity
    {
        [FieldOffset(0)]
        internal readonly int version;         // версия база данных
        [FieldOffset(4)]
        internal readonly Name name;        // название/префикс для базы данных [32]; 
        [FieldOffset(36)] 
        internal readonly ulong timeStamp;          // время создания базы данных
        [FieldOffset(44)] 
        internal readonly int records;         // общее количество записей
        [FieldOffset(48)] 
        internal readonly uint offsetRanges;      // смещение относительно начала файла до начала списка записей с геоинформацией
        [FieldOffset(52)] 
        internal readonly uint offsetCities;    // смещение относительно начала файла до начала индекса с сортировкой по названию городов
        [FieldOffset(56)] 
        internal readonly uint offsetLocations;   // смещение относительно начала файла до начала списка записей о местоположении

        public int Records => records;
        public int OffsetRanges => (int)offsetRanges;
        public int OffsetCities => (int)offsetCities;
        public int OffsetLocations => (int)offsetLocations;
    }
}
