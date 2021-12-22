using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Endpoints.LocationEndpoint
{
    public class GetByIpLocationResponse
    {
        public IpLocationDto[] IpLocations { get; set; }
    }
}
