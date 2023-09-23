using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.DTOs.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel(Guid id, string name, int gender, DateTime birthdate, string phone)
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
    }
}
