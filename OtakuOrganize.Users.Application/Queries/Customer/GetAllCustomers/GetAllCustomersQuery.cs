using MediatR;
using OtakuOrganize.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries;

public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
{
}
