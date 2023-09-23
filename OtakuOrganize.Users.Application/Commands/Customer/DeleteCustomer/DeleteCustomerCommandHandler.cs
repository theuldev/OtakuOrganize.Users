using System.IO;
using MediatR;
using OtakuOrganize.Users.Domain.Interfaces;
using OtakuOrganize.Users.Infra.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtakuOrganize.Users.Application.Extensions;

namespace OtakuOrganize.Users.Application.Commands.Customer.DeleteUser
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerRepository repository;
        private readonly IMessageBusClient messageBus;
        public DeleteCustomerCommandHandler(ICustomerRepository _repository, IMessageBusClient _messageBus)
        {
            messageBus = _messageBus;
            repository = _repository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var routingKey = request.GetType().Name.ToDashCase();
            messageBus.Publish(request,routingKey,"customer-service");
            await repository.Delete(request.Id);
            return Unit.Value;
        }
    }
}
