namespace Application.Models.Identity;

public class User
{
    public string Id { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set;} = String.Empty;
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime? DateCreated { get; set; }
    
    }