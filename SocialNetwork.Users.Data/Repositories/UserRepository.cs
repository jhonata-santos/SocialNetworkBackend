using Dapper;
using System.Data;
using SocialNetwork.Users.Domain.Entities;
using SocialNetwork.Users.Domain.Interfaces;

namespace SocialNetwork.Users.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;

    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("O ID deve ser um número positivo.", nameof(id));


        string sql = $"SELECT * FROM USERS WHERE ID = {id}";
        var result = await _connection.QuerySingleOrDefaultAsync<User>(sql);
        return result;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        string sql = "SELECT * FROM USERS";
        return await _connection.QueryAsync<User>(sql);
    }
}