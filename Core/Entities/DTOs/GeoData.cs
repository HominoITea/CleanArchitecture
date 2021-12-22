using Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Index = Core.Entities.Models.Index;

namespace Core.Entities.DTOs
{
    public class GeoData : Entity
    {
        public IpRange[] IpRanges { get; }
        public Location[] Locations { get; }
        private Index[] Indices { get; }

        public Header Header { get; private set; }
        public GeoData(Header header, IpRange[] ipRanges, Location[] locations, Index[] indices)
        {
            Header = header;
            IpRanges = ipRanges;
            Locations = locations;
            Indices = indices;
        }
    }
}
