using OtakuOrganize.Users.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Entities
{
    public class Customer : IEntityBase
    {
        public Customer()
        {
            
        }

        public Customer(string name, int gender, DateTime birthdate, string phone, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Gender = gender;
            Birthdate = birthdate;
            Phone = phone;
            UserId = userId;
        }

        public Customer(Guid id, string name, int gender, DateTime birthdate, string phone)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Birthdate = birthdate;
            Phone = phone;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Gender { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Phone { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; set; }

    }
}