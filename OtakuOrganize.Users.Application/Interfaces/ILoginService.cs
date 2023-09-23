using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string>Login(string email, string password);
    }
}
