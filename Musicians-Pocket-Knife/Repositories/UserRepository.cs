using Dapper;
using Microsoft.Data.SqlClient;
using Musicians_Pocket_Knife.Models;
using System.Data;

namespace Musicians_Pocket_Knife.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string CreateNewUserStoredProcedure = "[MPKDB].[dbo].[CreateNewUser]";

        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<User?> CreateNewUserAsync(User createUserRequest)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new { FirstName = createUserRequest.FirstName, LastName = createUserRequest.LastName, GoogleId = createUserRequest.GoogleId };
                var userId = await connection.QuerySingleAsync<int>(CreateNewUserStoredProcedure, parameters, commandType: CommandType.StoredProcedure);

                if (userId == -1)
                {
                    return null;
                }

                return new User
                {
                    Id = userId,
                    FirstName = createUserRequest.FirstName,
                    LastName = createUserRequest.LastName,
                    GoogleId = createUserRequest.GoogleId
                };
            }
        }
    }
}
