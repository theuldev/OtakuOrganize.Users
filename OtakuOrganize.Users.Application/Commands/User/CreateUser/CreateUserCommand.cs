using MediatR;
using OtakuOrganize.Users.Application.DTOs.ViewModels;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Enums;

namespace OtakuOrganize.Users.Application.Commands.CreateUser
{

    public class CreateUserCommand : IRequest<UserViewModel>
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }

}