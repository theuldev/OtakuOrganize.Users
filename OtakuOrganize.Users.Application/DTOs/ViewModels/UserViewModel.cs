using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consul;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Enums;

namespace OtakuOrganize.Users.Application.DTOs.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(Guid id, string username, string email, string password, DateTime createAt, DateTime loggedTime, RoleType role)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            CreateAt = createAt;
            LoggedTime = loggedTime;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime LoggedTime { get; private set; }
        public RoleType Role { get; private set; }


        public static UserViewModel FromEntity(User user){
             return new(user.Id, user.Username, user.Email, user.Password, user.CreatedAt, user.LoggedTime, user.Role);
        }
    }
}
