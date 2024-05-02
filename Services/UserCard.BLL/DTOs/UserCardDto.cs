namespace UserCard.BLL.DTOs;

public class UserCardDto
{
    public string UserName { get; set; } = null!;
    public int? Bonuses { get; set; }
        
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string? AddressLine { get; set; }
    public string Country { get; set; } = null!;
    public string State { get; set; } = null!;
    public string? ZipCode { get; set; }
}