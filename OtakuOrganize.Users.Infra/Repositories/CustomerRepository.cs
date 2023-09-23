using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OtakuOrganize.Users.Domain.Entities;
using OtakuOrganize.Users.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task Create(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "INSERT INTO Customers(id,name,gender,birthdate,phone, userid) values (@Id,@Name,@Gender,@Birthdate,@Phone, @UserId)";
                await connection.ExecuteAsync(command, customer);
            }
        }

        public async Task Delete(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "DELETE FROM Customers WHERE id = @Id";
                await connection.ExecuteAsync(command, new { Id = id });
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {

                var query = "SELECT c.Id, name,gender,birthdate,phone, c.UserId, u.Id, u.Username ,u.Email ,u.Password, u.Createdat, u.LoggedTime, u.Role FROM Customers c INNER JOIN Users u ON c.UserId = u.Id";
                var customers = await connection.QueryAsync<Customer, User, Customer>(query, (customer, user) =>
                {
                    customer.User = user;
                    return customer;
                },
                splitOn: "UserId");
                if (customers == null) throw new NullReferenceException();
                return customers.OrderBy(c => c.Id).ToList();


            }
        }

        public async Task<Customer> GetById(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                var query = "SELECT c.Id, name,gender,birthdate,phone, c.UserId, u.Id, u.Username ,u.Email ,u.Password, u.Createdat, u.LoggedTime, u.Role FROM Customers c INNER JOIN Users u ON c.UserId = u.Id  WHERE c.Id = @Id";
                var customer = await connection.QueryAsync<Customer,User,Customer>(query,(customer ,user ) => {
                    customer.User = user;
                    return customer ?? throw new NullReferenceException();
                }, new {Id = id});
                return customer.Single();
                
            }
        }

        public async Task Update(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "UPDATE Customers SET name = @name , gender = @gender,birthdate = @birthdate, phone = @phone WHERE id = @Id ";
                await connection.ExecuteAsync(command, customer);
            }
        }
    }
}
