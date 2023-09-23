using MediatR;
using OtakuOrganize.Users.Application;
using OtakuOrganize.Users.Application.Extensions;
using OtakuOrganize.Users.Application.Services.Integration;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using OtakuOrganize.Users.Infra.MessageBus;
using OtakuOrganize.Users.Infra.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository repository;
        private readonly IMessageBusClient messageBusClient;
        private readonly IServiceDiscoveryService serviceDiscovery;

        public CreateCustomerHandler(ICustomerRepository repository, IMessageBusClient messageBusClient, IServiceDiscoveryService serviceDiscovery)
        {
    
            this.repository = repository;
            this.messageBusClient = messageBusClient;
            this.serviceDiscovery = serviceDiscovery;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Name, request.Gender, request.Birthdate, request.Phone, request.UserId);

            var routingKey = request.GetType().Name.ToDashCase();

            messageBusClient.Publish(customer, routingKey,"customer-service");
            await repository.Create(customer);
            return await Task.FromResult(customer);
        }
    }
}
