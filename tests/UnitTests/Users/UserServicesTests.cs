using Application.Models.Identity;
using Identity.Services;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Users;

public class UserServicesTests
{
    private readonly Mock<UserService> _mockRepo;
    
    public UserServicesTests()
    {
        _mockRepo = new Mock<UserService>();
        
        // set up the user service
        // get user info
        var testId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
        var user = MockUserServices.GetUserServices()[0];
        var response = new User()
        {
            // copy the properties in user
            Firstname = user.FirstName,
            Id = user.Id,
            Email = user.Email,
            Lastname = user.LastName,
            BirthDate = user.BirthDate,
            Bio = user.Bio
        };
        // _mockRepo.Setup(x => x.GetUserById(testId)).ReturnsAsync(response);

        // var result = await _mockRepo.Object.GetUserById(testId);
        
    }


    [Fact]
    public async void GetUserInfoTest()
    {
        // get the user info of id
        
        var user = await  _mockRepo.Object.GetUserById("8e445865-a24d-4543-a6c6-9443d048cdb9");
        
        // shouldy assert
        user.ShouldNotBeNull();
        user.ShouldBeOfType<User>();
        user.Firstname.ShouldBeEquivalentTo("System");
    }
    
}