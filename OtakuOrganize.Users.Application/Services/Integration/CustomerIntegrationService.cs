using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtakuOrganize.Users.Infra.ServiceDiscovery;

namespace OtakuOrganize.Users.Application.Services.Integration
{
    public class CustomerIntegrationService : ICustomerIntegrationService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerIntegrationService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task GetRequiredUri(IServiceDiscoveryService serviceDiscovery, int? id){
        var animeUrl = await serviceDiscovery.GetServiceUri("AnimeServices",$"/api/anime/{id}");

        if(animeUrl == null) throw new NullReferenceException("animeUrl nulo");

        var httpClient = httpClientFactory.CreateClient();
        using(var response = httpClient.GetAsync(animeUrl)){}
        }
    }
}