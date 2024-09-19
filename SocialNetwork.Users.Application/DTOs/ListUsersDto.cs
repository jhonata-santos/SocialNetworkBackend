using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Users.Application.DTOs;

public class ListUsersDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string CPF { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
}