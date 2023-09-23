using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Entities
{
    public interface IEntityBase
    {
         Guid Id { get; }
    }
}
