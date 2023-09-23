using OtakuOrganize.Users.Application.Interfaces;
using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Services
{
    public class SecurityService : ISecurityService
    {
        public Task<string> EncryptPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var passEncrypt = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return Task.FromResult(passEncrypt);
        }
        public Task<string> DecryptPassword(string passEncrypt)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(passEncrypt);
            return Task.FromResult(password);
        }
        public Task<bool> VerifyPassword(string password, string passToVerify)
        {

            var result = BCrypt.Net.BCrypt.Verify(password, passToVerify);
            return Task.FromResult(result);

        }
    }
}
