using MediatR;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries;

public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, Customer>
{
    private readonly ICustomerRepository repository;

    public GetByIdCustomerQueryHandler(ICustomerRepository _repository)
    {
        this.repository = _repository;
    }

    public async Task<Customer> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = await repository.GetById(request.Id);
        return await Task.FromResult(response);
    }
}
