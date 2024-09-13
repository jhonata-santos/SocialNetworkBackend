namespace SocialNetwork.Users.Application.DTOs;

public class UserDto
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string CPF { get; set; }
    public required string Email { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}