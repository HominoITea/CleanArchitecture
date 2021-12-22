using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class GeoDataBinary : Entity
    {
        public Header Header { get; set; }
        public IpRange[] IpRanges { get; set; }
        public LocationBytes[] Locations { get; set; }
        public int[] Indices { get; set; }
    }
}
