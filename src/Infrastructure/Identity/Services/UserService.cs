using Application.Contracts.Identity;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> GetUserById(string userId)
    {
        // get the user using the manager 
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
            return new User()
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                Firstname = user.FirstName!,
                Lastname = user.LastName!,
                Bio = user.Bio,
                BirthDate = user.BirthDate,
                ProfilePicture = user.ProfilePicture,
                DateCreated = user.DateCreated
            };
        throw new NotFoundException("User", userId);
    }

    public Task UpdateUser(UpdateUserDto user)
    {
        // find the user in the identity database
        var userToUpdate = _userManager.FindByIdAsync(user.Id.ToString());
        
        if (user == null)
            throw new NotFoundException("User", user!.Id);
        
        // assign the values of user to the userToUpdate
        userToUpdate.Result!.FirstName = user.Firstname;
        userToUpdate.Result.LastName = user.Lastname;
        userToUpdate.Result.Bio = user.Bio;
        userToUpdate.Result.BirthDate = user.BirthDate;
        userToUpdate.Result.Email = user.Email;
        userToUpdate.Result.UserName = user.UserName;
        
        // update the user
        var result = _userManager.UpdateAsync(userToUpdate.Result);

        if (!result.IsCompletedSuccessfully)
        {
            var error = result.Exception?.InnerException?.Message ?? "Error updating user";
            throw new BadRequestException(error);
        }
            
        // return a Unit rapped in task
        return Task.FromResult(Unit.Value);
    }

    public Task DeleteUser(string id)
    {
        // get the user
        var user = _userManager.FindByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException("User", id);
        
        // delete the user
        var result = _userManager.DeleteAsync(user.Result!);

        return Task.FromResult(Unit.Value);
    }
    
}