using OtakuOrganize.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public Task Create(Customer customer);
        public Task Update(Customer customer);
        public Task Delete(Guid id);
        public Task<Customer> GetById(Guid id);
        public Task<IEnumerable<Customer>> GetAllAsync();
    }
}
