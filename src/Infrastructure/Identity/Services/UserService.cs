using System.Security.Claims;
using Application.Contracts.Identity;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;
    public UserService(UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
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
                DateCreated = user.DateCreated,
                ConnectionId = user.ConnectionId!
            };
        throw new NotFoundException("User", userId);
    }

    public async Task UpdateUser(UpdateUserDto user)
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
        var result = await _userManager.UpdateAsync(userToUpdate.Result);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(er => er.Description).ToArray();
            
            throw new BadRequestException(string.Join(Environment.NewLine, errors));
        }
    }

    public async Task DeleteUser(string id)
    {
        // get the user
        var user =await  _userManager.FindByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException("User", id);
        
        //signout user
        await _authService.SignOut();   
        
        // delete the user
        var result = await  _userManager.DeleteAsync(user);
        
        
        
        
    }

    public async Task UploadProfilePicture(string userId, byte[] picture)
    {
        // check if user is logged in by current user
        // if (userId != this.userId)
        //     throw new UnauthorizedAccessException("You are not authorized to perform this action.");
        // get the user
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)    
        {
            throw new NotFoundException("User", userId);
        }
        
        // update the user
        user.ProfilePicture = picture;
        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(er => er.Description).ToArray();
            
            throw new BadRequestException(string.Join(Environment.NewLine, errors));
        }
        
    }

    public async Task<byte[]> GetProfilePicture(string userId)
    {
        // get the user
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            throw new NotFoundException("User", userId);
        
        if (user.ProfilePicture == null)
            throw new NotFoundException("Profile Picture", userId);
        
        // get the profile picture
        return user.ProfilePicture;
    }

    public async Task<bool> Exists(string userId)
    {
        try
        {
            await GetUserById(userId);

        }
        catch (NotFoundException)
        {
            return false;
        }

        return true;
    }
}