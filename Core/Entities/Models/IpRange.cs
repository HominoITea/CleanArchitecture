using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Models
{
    public readonly struct IpRange : IEntity, IComparable<uint>
    {
        public readonly uint IpFrom { get; }
        public readonly uint IpTo { get; }
        public readonly uint LocationIndex { get; }
        public int CompareTo(uint ip)
        {
            int re = ip.CompareTo(IpTo) ^ ip.CompareTo(IpFrom);
            if (ip > IpTo)
            {
                return 1;
            }
            if (ip < IpFrom)
            {
                return -1;
            }
            return 0;
        }
    }
}
