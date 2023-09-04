using Application.Contracts.Email;
using Application.Contracts.Identity;
using Application.Models.Email;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly IEmailSender _emailSender;

    public AuthController(IAuthService authenticationService, IEmailSender emailSender)
    {
        _authenticationService = authenticationService;
        _emailSender = emailSender;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
        
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var response = await _authenticationService.Register(request);
        var verificationLink = Url.Action("VerifyEmail", "Auth", new { userId = response.UserId, token = response.Token }, Request.Scheme);
        await _emailSender.SendEmail(new EmailMessage()
        {
            Body = "Please verify your email by clicking on the link below: <br/>" + verificationLink,
            Subject = "Email Verification",
            To = request.Email
        });
        return Ok(response);
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail(string userId, string token)
    {
        var result = await _authenticationService.VerifyEmail(userId, token);
        if (!result)
        {   
            return BadRequest("Verification failed");
        }

        return Ok("Thank you for verifying your email address");
    }
    
    [HttpPost("forget-password")]
    public async Task<ActionResult<ForgetPasswordResponse>> ForgetPassword(string email)
    {
        var response = await _authenticationService.ForgetPassword(email);
        var verificationLink = Url.Action("ResetPassword", "Auth", new { userId = response.UserId, token = response.Token }, Request.Scheme);
        await _emailSender.SendEmail(new EmailMessage()
        {
            Body = "Please reset your password by clicking on the link below: <br/>" + verificationLink,
            Subject = "Reset Password",
            To = email
        });
        return Ok(response);
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await _authenticationService.ResetPassword(request);
        if (!result)
        {
            return BadRequest("Reset password failed");
        }

        return Ok("Thank you for resetting your password");
    }
}