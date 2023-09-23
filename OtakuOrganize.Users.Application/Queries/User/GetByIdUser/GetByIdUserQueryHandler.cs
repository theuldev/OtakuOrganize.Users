using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries.GetByIdUser
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, User>
    {
        private readonly IUserRepository userRepository;
        public GetByIdUserQueryHandler(IUserRepository _userRepository)
        {   
            userRepository = _userRepository;
            
        }
        public async Task<User> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
           var response = await userRepository.GetById(request.Id);
            return await Task.FromResult(response);
        }
    }
}
