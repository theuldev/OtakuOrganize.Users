using MediatR;
using OtakuOrganize.Users.Application.Queries.GetAllUsers;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository repository;

    public GetAllCustomersQueryHandler(ICustomerRepository _repository)
    {
        this.repository = _repository;
    }

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync();
        return await Task.FromResult(customers);
    }
}
