using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtakuOrganize.Users.Application.DTOs.ViewModels;
using OtakuOrganize.Users.Application.Interfaces;
using OtakuOrganize.Users.Domain.Exceptions;
using OtakuOrganize.Users.Infra.MessageBus;
using OtakuOrganize.Users.Application.Extensions;

namespace OtakuOrganize.Users.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly IUserRepository repository;
        private readonly ISecurityService securityService;
        private readonly IMessageBusClient messageBus;



        public CreateUserCommandHandler(IUserRepository _repository, ISecurityService _securityService, IMessageBusClient _messageBus)
        {
            repository = _repository;
            securityService = _securityService;
            messageBus = _messageBus;
        }

        public async Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            request.Password = securityService.EncryptPassword(request.Password).Result;
            var user = new User(request.Username, request.Password, request.Email, request.Role);
        

            if (repository.GetUserWithEmail(request.Email).Result != null) throw new EmailAlreadyRegisteredException();
            var routingKey = request.GetType().Name.ToDashCase();
            messageBus.Publish(request, routingKey, "user-service");
            await repository.Create(user);

            var model = UserViewModel.FromEntity(user);
            return await Task.FromResult(model);

        }

    }
}