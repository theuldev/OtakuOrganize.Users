using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Commands.Customer.DeleteUser
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public Guid Id {  get; set; }
    }
}
