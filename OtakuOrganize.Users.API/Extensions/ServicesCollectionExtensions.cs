using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using OtakuOrganize.Users.Application.Commands.CreateUser;
using OtakuOrganize.Users.Application.Commands.DeleteUser;
using OtakuOrganize.Users.Application.Commands.UpdateUser;
using OtakuOrganize.Users.Application.Interfaces;
using OtakuOrganize.Users.Application.Queries.GetAllUsers;
using OtakuOrganize.Users.Application.Queries.GetByIdUser;
using OtakuOrganize.Users.Application.Services;
using OtakuOrganize.Users.Application.Validations;
using OtakuOrganize.Users.Domain.Interfaces;
using OtakuOrganize.Users.Domain.Repositories;
using OtakuOrganize.Users.Infra.Extensions;
using OtakuOrganize.Users.Infra.MessageBus;
using OtakuOrganize.Users.Infra.Repositories;
using RabbitMQ.Client;
using System.Text;

namespace OtakuOrganize.Users.API.Extensions
{

    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);


            });
            services
                .AddValidatorsFromAssemblyContaining(typeof(CreateUserValidator)).AddFluentValidationAutoValidation();
            services
              .AddValidatorsFromAssemblyContaining(typeof(UpdateUserValidator)).AddFluentValidationAutoValidation();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILoginService, LoginService>();





            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]))

                };
            });

            services.AddAuthorization();



            services.AddMessageBus();


            return services;

        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddConsulConfig(configuration);
            return services;
        }
        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
    

            var connection = factory.CreateConnection("customer-service-producer");

            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();
            return services;
        }


     
    }
}