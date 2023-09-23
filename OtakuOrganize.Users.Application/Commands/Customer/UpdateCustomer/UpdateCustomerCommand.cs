using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtakuOrganize.Users.Domain.Entities;
namespace OtakuOrganize.Users.Application
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
    }
}
