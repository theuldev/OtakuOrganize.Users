using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtakuOrganize.Users.Infra.ServiceDiscovery;

namespace OtakuOrganize.Users.Infra.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = config.GetSection("Consul:Host").Value ?? throw new NullReferenceException();
                consulConfig.Address = new Uri(address);
            }));
            
            services.AddTransient<IServiceDiscoveryService,ServiceDiscoveryService>();
            return services;
        }
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var registration = new AgentServiceRegistration
            {
                ID = $"customer-service-{Guid.NewGuid()}",
                Name = "CustomerServices",
                Address = "localhost",
                Port = 5237
            };

            try
            {
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

                lifeTime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
                    Console.WriteLine($"Service with Id {registration.ID} and Name {registration.Name} deregistered in Consul");


                });

                Console.WriteLine($"Service with Id {registration.ID} and Name {registration.Name} registered in Consul");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return app;


        }
    }


}
