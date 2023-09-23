using OtakuOrganize.Users.Application.Commands.UpdateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Validations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty()
              .NotNull()
              .WithMessage("O id não pode ser nulo");

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
