using Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using CityIndex = Core.Entities.Models.CityIndex;

namespace Core.Entities.DTOs
{
    public class GeoData : Entity
    {
        public IpRange[] IpRanges { get; }
        public Location[] Locations { get; }
        private CityIndex[] Indices { get; }

        public Header Header { get; private set; }
        public GeoData(Header header, IpRange[] ipRanges, Location[] locations, CityIndex[] indices)
        {
            Header = header;
            IpRanges = ipRanges;
            Locations = locations;
            Indices = indices;
        }
    }
}
