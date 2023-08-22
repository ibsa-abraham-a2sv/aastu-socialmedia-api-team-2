using Application.DTOs.User;
using Application.Models.Identity;

namespace Application.Contracts.Identity;

public interface IUserService
{
    // get the user by id
    public Task<User> GetUserById(string userId);
    // update the user
    public Task UpdateUser(UpdateUserDto user);
    // delete the user
    public Task DeleteUser(string id);
    
}