using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IHeader
    {
        public int Records { get; }
        public int LocationOffset { get; }
        public int IpRangeOffset { get; }
        public int CityIndexOffset { get; }
    }
}
