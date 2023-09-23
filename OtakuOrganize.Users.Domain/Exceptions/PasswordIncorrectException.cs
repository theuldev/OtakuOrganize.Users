using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Exceptions
{
    public class PasswordIncorrectException : Exception
    {
        public PasswordIncorrectException() : base("Password not registered")
        {
            
        }
    }
}
