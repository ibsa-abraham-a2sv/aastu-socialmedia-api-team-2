using Application.Models.Identity;

namespace Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
    Task SignOut();
    Task<bool> VerifyEmail(string userId, string token);
    Task<ForgetPasswordResponse> ForgetPassword(string email);
    Task<bool> ResetPassword(ResetPasswordRequest request);
}