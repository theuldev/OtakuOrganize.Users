using MediatR;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly ICustomerRepository repository;
    public UpdateCustomerCommandHandler(ICustomerRepository _repository)
    {
        repository = _repository;
    }

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Id, request.Name, request.Gender, request.Birthdate, request.Phone);
        await repository.Update(customer);
        return await Task.FromResult(customer);
    }
}
