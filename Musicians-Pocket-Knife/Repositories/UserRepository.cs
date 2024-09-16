using Dapper;
using Microsoft.Data.SqlClient;
using Musicians_Pocket_Knife.Helpers;
using Musicians_Pocket_Knife.Models;
using System.Data;

namespace Musicians_Pocket_Knife.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string CreateNewUserStoredProcedure = "[MPKDB].[dbo].[CreateNewUser]";
        private readonly string VerifyExistingUserStoredProcedure = "[MPKDB].[dbo].[VerifyExistingUser]";

        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool? VerifyExistingUser(VerifyExistingUserRequest request)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new 
                { 
                    GoogleId = request.GoogleId!
                };

                try
                {
                    var response = connection.QuerySingleOrDefault<int>(VerifyExistingUserStoredProcedure, parameters, commandType: CommandType.StoredProcedure);
                    return RepositoryHelper.ConvertIntToBool(response);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Add Logger here, error in db request");
                    return null;
                }
            }
        }

        public async Task<User?> CreateNewUserAsync(User createUserRequest)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new 
                { 
                    FirstName = createUserRequest.FirstName,
                    LastName = createUserRequest.LastName,
                    GoogleId = createUserRequest.GoogleId 
                };

                var userId = await connection.QuerySingleAsync<int>(CreateNewUserStoredProcedure, parameters, commandType: CommandType.StoredProcedure);

                if (userId == -1)
                {
                    return null;
                }

                return new User
                {
                    FirstName = createUserRequest.FirstName,
                    LastName = createUserRequest.LastName
                };
            }
        }
    }
}
