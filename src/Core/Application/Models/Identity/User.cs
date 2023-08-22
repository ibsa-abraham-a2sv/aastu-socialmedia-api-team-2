namespace SocialAsync.Application.Models.Identity;

public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set;}
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime? DateCreated { get; set; }
    
    }