using OtakuOrganize.Users.Application.Interfaces;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Exceptions;
using OtakuOrganize.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository repository;
        private readonly ISecurityService securityService;
        private readonly IAuthService authService;
        public LoginService(IUserRepository _repository, ISecurityService _securityService, IAuthService _authService)
        {
            repository = _repository;
            securityService = _securityService;
            authService = _authService;

        }
        public async Task<string> Login(string email, string password)
        {
            var model = await repository.GetUserWithEmail(email);

            if (model == null) throw new NullReferenceException();

            var verifyToken = await securityService.VerifyPassword(password, model.Password);
            if (!verifyToken) throw new PasswordIncorrectException();
            var token = authService.GenerateJwtToken(model.Email, model.Role);

            await repository.UpdateLoggedTime(model);
            return token;



        }
    }
}
