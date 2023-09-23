using OtakuOrganize.Users.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, RoleType role);
    }
}
