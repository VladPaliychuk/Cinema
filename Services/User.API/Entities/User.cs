namespace User.API.Entities;

public class User
{
    //TODO split user on User and Role
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}