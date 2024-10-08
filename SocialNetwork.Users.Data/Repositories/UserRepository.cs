﻿using Dapper;
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


        string sql = $"SELECT * FROM Users WHERE ID = {id}";
        var result = await _connection.QuerySingleOrDefaultAsync<User>(sql);
        return result;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        string sql = "SELECT * FROM Users WHERE Available = 1";
        return await _connection.QueryAsync<User>(sql);
    }

    public async Task<int> CreateAsync(User user)
    {
        string sql = $"INSERT INTO Users (NAME, DATE_OF_BIRTH, CPF, EMAIL, PASSWORD, CREATE_AT, UPDATE_AT, AVAILABLE) " +
                     $"VALUES ('{user.Name}', '{user.DateOfBirth:yyyy-MM-dd HH:mm:ss}', '{user.CPF}', '{user.Email}', '{user.Password}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', 1);" +
                     $"SELECT LAST_INSERT_ID();";
        int userId = await _connection.ExecuteScalarAsync<int>(sql);
        return userId;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        string sql = $"UPDATE Users " +
                     $"SET AVAILABLE = {user.Available}, UPDATE_AT = '{user.UpdateAt:yyyy-MM-dd HH:mm:ss}' " +
                     $"WHERE Id = {user.Id}";
        int rowsAffected = await _connection.ExecuteAsync(sql);
        return rowsAffected > 0;
    }
}