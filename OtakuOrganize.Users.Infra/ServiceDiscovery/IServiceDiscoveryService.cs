using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Infra.ServiceDiscovery
{
    public interface IServiceDiscoveryService
    {
        public Task<Uri> GetServiceUri(string service, string requestUrl);
    }
}