using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtakuOrganize.Users.Infra.ServiceDiscovery;

namespace OtakuOrganize.Users.Application.Services.Integration
{
    public interface ICustomerIntegrationService
    {
       public Task GetRequiredUri(IServiceDiscoveryService serviceDiscovery, int? id);
    }
}