using MediatR;
using OtakuOrganize.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries;

public class GetByIdCustomerQuery : IRequest<Customer>
{
    public GetByIdCustomerQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
