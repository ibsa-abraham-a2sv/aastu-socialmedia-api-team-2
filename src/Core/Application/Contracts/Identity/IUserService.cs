using Application.DTOs.User;
using SocialAsync.Application.Models.Identity;

namespace SocialAsync.Application.Contracts.Identity;

public interface IUserService
{
    // get the user by id
    public Task<User> GetUserById(string userId);
    // update the user
    public Task UpdateUser(UpdateUserDto user);
    // delete the user
    public Task DeleteUser(string id);
    
}