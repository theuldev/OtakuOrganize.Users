using OtakuOrganize.Users.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Application.Queries.GetByIdUser
{
    public class GetByIdUserQuery : IRequest<User>
    {
        public GetByIdUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
