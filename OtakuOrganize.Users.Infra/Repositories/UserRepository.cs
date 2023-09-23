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

namespace OtakuOrganize.Users.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(User user)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var command = "INSERT INTO Users (id,username,email,password, loggedtime,createdat, role) VALUES (@Id, @Username,@Email,@Password, @Loggedtime, @Createdat, @Role)";
                await connection.ExecuteAsync(command, user);

            }
        }

        public async Task Delete(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "DELETE from Users WHERE id = @Id";
                await connection.ExecuteAsync(command, new { Id = id });
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT id,username,email,password,loggedtime,createdat from Users ";
                var users = await connection.QueryAsync<User>(query);
                return users.OrderBy(c => c.Id).ToList();
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT id,username,email,password,loggedtime,createdat from Users where id = @Id";
                var user = await connection.QuerySingleAsync<User>(query, new { Id = id });
                return user ?? throw new NullReferenceException();
            }
        }

        public async Task<User> GetUserWithEmail(string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT id,username,email,password,loggedtime,createdat,role from Users where email = @Email";
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
                return user;
            }

        }

        public async Task Update(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "UPDATE Users SET username = @username, email = @email, password = @password, loggedtime = @loggedtime where id = @id";

                await connection.ExecuteAsync(command, user);

            }
        }

        public async Task UpdateLoggedTime(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = "UPDATE Users SET loggedtime = @loggedtime where username = @username AND password = @password";
                await connection.ExecuteAsync(command, new{ username = user.Username, password = user.Password, loggedtime = DateTime.Now });
            }
        }
    }
}
