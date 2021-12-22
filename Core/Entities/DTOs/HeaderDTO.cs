using Core.Converters;
using Core.Entities.Models;

namespace Core.Entities.DTOs
{
    public class HeaderDTO : Entity
    {
        public Header DbHeader { get; }
        public int Version => DbHeader.version; // версия база данных
        public char[] Name => Converter.MapObjectToCharArray(DbHeader.name); // название/префикс для базы данных [32];         
        public ulong TimeStamp => DbHeader.timeStamp;          // время создания базы данных
        public int Records => DbHeader.records;    // общее количество записей
        public uint OffsetRanges => DbHeader.offsetRanges;     // смещение относительно начала файла до начала списка записей с геоинформацией
        public uint OffsetCities => DbHeader.offsetCities;    // смещение относительно начала файла до начала индекса с сортировкой по названию городов
        public uint OffsetLocations => DbHeader.offsetLocations;   // смещение относительно начала файла до начала списка записей о местоположении

        public HeaderDTO()
        {

        }
        public HeaderDTO(Header dbHeader)
        {
            DbHeader = dbHeader;
        }
    }
}
