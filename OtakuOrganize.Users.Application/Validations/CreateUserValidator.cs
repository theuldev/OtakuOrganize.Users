using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtakuOrganize.Users.Application.Commands.CreateUser;

namespace OtakuOrganize.Users.Application.Validations
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email incorreto");

            RuleFor(x => x.Username)
                .NotEmpty()
                 .WithMessage("O username não pode ser vazio")
                .NotNull()
                .MaximumLength(20)
                .WithMessage("O username é tem tamanho maior que 20 caracteres");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("A senha não pode ser nula");
        }
    }
}
