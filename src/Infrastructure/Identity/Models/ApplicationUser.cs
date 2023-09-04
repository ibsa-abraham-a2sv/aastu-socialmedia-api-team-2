using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? PasswordComfirmation { get; set; }
        public string? ConnectionId { get; set; }
    }
}
