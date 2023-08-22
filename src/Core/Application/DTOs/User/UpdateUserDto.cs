namespace Application.DTOs.User;

public class UpdateUserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set;}
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }

}