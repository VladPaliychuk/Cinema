using System.ComponentModel.DataAnnotations;

namespace User.BLL.DTOs;
public class UserDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; } = null!;
}
