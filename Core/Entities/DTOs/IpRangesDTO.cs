using Core.Entities.DTOs;
using Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class IpRangesDTO : Entity
    {        
        public IpRange[] IpRanges { get; }     
        public IpRangesDTO(IpRange[] dbIpRanges)
        {
            IpRanges = dbIpRanges;
        }
        public uint IpFrom(int position) => IpRanges[position].IpFrom;  // смещение относительно начала файла до начала списка записей с геоинформацией
        public uint IpTo(int position) => IpRanges[position].IpTo;    // смещение относительно начала файла до начала индекса с сортировкой по названию городов        public uint locationIndex => HeaderRaw.offsetLocations;   // смещение относительно начала файла до начала списка записей о местоположении
        public uint LocationIndex(int position) => IpRanges[position].LocationIndex;   // смещение относительно начала файла до начала списка записей о местоположении
    }
}
