using OtakuOrganize.Users.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Entities
{
    public class User : IEntityBase
    {
        public User()
        {

        }

        public User(string username, string password, string email, RoleType role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Email = email;
            CreatedAt = DateTime.Now;
            LoggedTime = DateTime.Now;
            Role = role;
        }

        public User(Guid id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            LoggedTime = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LoggedTime { get; private set; }
        public RoleType Role { get; private set;  }

    
    }
}