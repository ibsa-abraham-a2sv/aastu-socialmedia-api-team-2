using Application.Contracts.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace UnitTests.Mocks;

public static class MockUserServices
{
    public static List<ApplicationUser> GetUserServices()
    {
        //list of users
        var hasher = new PasswordHasher<ApplicationUser>();
        var users = new List<ApplicationUser>{
            new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null!, "P@ssword1"),
                EmailConfirmed = true,
                BirthDate = DateTime.UtcNow,
                
            },
            new ApplicationUser
            {
                Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null!, "P@ssword1"),
                EmailConfirmed = true,
                BirthDate = DateTime.UtcNow
            }
        };
        return users;
    }

}