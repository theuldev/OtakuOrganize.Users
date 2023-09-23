using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository repository;
        public GetAllUsersQueryHandler(IUserRepository _repository)
        {
            repository = _repository;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await repository.GetAllAsync();
            return await Task.FromResult(response);
        }
    }
}
