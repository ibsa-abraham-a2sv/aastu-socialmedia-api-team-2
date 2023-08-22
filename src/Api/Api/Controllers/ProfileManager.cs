using Application.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class ProfileManager : ControllerBase
{
    private readonly IUserService _userService;

    public ProfileManager(IUserService userService)
    {
        _userService = userService;
    }
    
    
}