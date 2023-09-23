using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using OtakuOrganize.Users.Infra.MessageBus;
using OtakuOrganize.Users.Application.Extensions;

namespace OtakuOrganize.Users.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository repository;
        private readonly IMessageBusClient messageBus;
        public DeleteUserCommandHandler(IUserRepository _repository, IMessageBusClient _messageBus)
        {
            repository = _repository;
            messageBus = _messageBus;
        }


        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var routingKey = request.GetType().Name.ToDashCase();
            
            messageBus.Publish(request, routingKey, "user-service");
            await repository.Delete(request.Id);
            return Unit.Value;

        }
    }
}
