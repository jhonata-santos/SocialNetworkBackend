namespace SocialNetwork.Users.Application.DTOs;

public class ListUsersDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string CPF { get; set; }
    public required string Email { get; set; }
}