
using Consul;

namespace OtakuOrganize.Users.Infra.ServiceDiscovery
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly IConsulClient consulClient;
        public ServiceDiscoveryService(IConsulClient _consulClient)
        {
            consulClient = _consulClient;

        }
        public async Task<Uri> GetServiceUri(string service, string requestUrl)
        {
            var allServices =   await consulClient.Agent.Services();
            var registeredService =  allServices.Response.Where(s => s.Value.Service.Equals(service))
            .Select(s => s.Value)
            .ToList();

            var currentService = registeredService.FirstOrDefault();

            Console.WriteLine($"Service Address: {currentService.Address}");
            var uri = $"http://{currentService.Address}:{currentService.Port} {requestUrl}";

            return new Uri(uri);
        }
    }
}