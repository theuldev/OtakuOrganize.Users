using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.DTOs.ViewModels
{
    public class AutheticateResponse
    {
        public AutheticateResponse(string token)
        {
            Token = token;
        }
        public string Token { get; private set; }
    }
}
