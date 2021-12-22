using Core.Entities;

namespace WebApi.Endpoints.LocationEndpoint
{
    public class IpLocationDto //: Entity
    {
        public uint IpFrom { get; set; }
        public uint IpTo { get; set; }
        public uint LocationIndex { get; set; }
    }
}