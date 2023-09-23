using OtakuOrganize.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task Create(User user);
        public Task Update(User user);  
        public Task Delete(Guid id);
        public Task<User> GetById(Guid id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetUserWithEmail(string email);
        public Task UpdateLoggedTime(User user);
    }
}
