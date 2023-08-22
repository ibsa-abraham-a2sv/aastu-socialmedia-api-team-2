namespace Application.DTOs.User;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }

}