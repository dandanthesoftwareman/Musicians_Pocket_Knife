using Dapper;
using Microsoft.Data.SqlClient;
using Musicians_Pocket_Knife.Models;
using System.Data;

namespace Musicians_Pocket_Knife.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string CreateNewUser = "[MPKDB].[dbo].[CreateNewUser]";

        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = @"Server=.\\sqlexpress; Database=MPKDB; Trusted_Connection=True;";
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<User?> CreateNewUserAsync(string googleId, string name)
        {
            var firstName = name.Split('_')[0];
            var lastName = name.Split('_')[1];

            using (var connection = CreateConnection())
            {
                string sql = CreateNewUser;

                var parameters = new { FirstName = firstName, LastName = lastName, GoogleId = googleId };
                var userId = await connection.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.StoredProcedure);

                if (userId == -1)
                {
                    return null;
                }

                return new User
                {
                    Id = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    GoogleId = googleId
                };
            }
        }
    }
}
