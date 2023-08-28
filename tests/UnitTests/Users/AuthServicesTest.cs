using Application.Contracts.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Users;

public class AuthServicesTest
{
    private readonly Mock<IAuthService> _mockRepo;
    
    public AuthServicesTest()
    {
        _mockRepo = new Mock<IAuthService>();
        
        // setup mock repo add the user to the array of users
        _mockRepo.Setup(x => x.Register(It.IsAny<RegistrationRequest>())).ReturnsAsync((RegistrationRequest request) =>
        {
            var users = MockUserServices.GetUserServices();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            
            var user = new ApplicationUser()
            {
                // copy all the element of the request to the user
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                BirthDate = request.BirthDate,
                PasswordHash = passwordHasher.HashPassword(null!, request.Password),
                EmailConfirmed = true,
                PasswordComfirmation = request.PasswordComfirmation,
            };
            users.Add(user);
            
            var response = new RegistrationResponse()
            {
                UserId = user.Id
            };
            return response;
        });
        
        // setup the mock to login
        _mockRepo.Setup(x => x.Login(It.IsAny<AuthRequest>())).ReturnsAsync( (AuthRequest request) =>
        {
            var users = MockUserServices.GetUserServices();
            var user = users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
            {
                throw new Exception($"User with {request.Email} not found.");
            }
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'.");
            }

            return new AuthResponse()
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Id = user.Id,
                Token = "token"
            };
        });

    }

    [Fact]
    public async Task RegisterTest()
    {
        // create an object of registration request
        var request = new RegistrationRequest()
        {
            UserName = "test",
            Email = "user@example.com",
            Password = "P@ssword1",
            PasswordComfirmation = "P@ssword1",
            FirstName = "Test",
            LastName = "User",
            BirthDate = DateTime.UtcNow

        };
        
        // create an object of registration response
        var response = new RegistrationResponse()
        {
            UserId = Guid.NewGuid().ToString()
        };
        
        
        

        // call the register method
        var result = await _mockRepo.Object.Register(request);
        
        // assert the result
        result.ShouldNotBeNull();
        result.ShouldBeOfType<RegistrationResponse>();

    }


    [Fact]
    public async Task LoginTest()
    {
        // create an object of registration request
        var request = new AuthRequest()
        {
            Email = "admin@localhost.com",
            Password = "P@ssword1",
        };
        
        // call the login
        var result = await _mockRepo.Object.Login(request);
        
        // shouldly assert the user
        result.ShouldNotBeNull();
        result.ShouldBeOfType<AuthResponse>();
        
        
    }
}