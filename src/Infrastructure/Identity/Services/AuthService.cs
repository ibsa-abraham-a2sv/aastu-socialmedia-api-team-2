using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using Application.Contracts.Email;
using Application.Contracts.Identity;
using Application.Exceptions;
using Application.Models.Email;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailSender _emailSender;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
      
            if (user == null)
            {
                throw new BadRequestException($"User with {request.Email} not found.");
            }
            
            if (user.EmailConfirmed == false)
            {
                throw new BadRequestException($"Email {request.Email} is not confirmed.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email!,
                UserName = user.UserName!
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new BadRequestException($"Username '{request.UserName}' already exists.");
            }
            
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                throw new BadRequestException($"Email {request.Email } already exists.");
            }
            
            
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                BirthDate = request.BirthDate,
                PasswordComfirmation = request.PasswordComfirmation,
            };
            
        
            var mailMessage = new EmailMessage()
            {
                To = request.Email,
                Subject = "Welcome to the Social Network",
                Body = "Welcome to the Social Network",
            };
         
            await _emailSender.SendEmail(mailMessage);
            // return new RegistrationResponse();
            var result = await _userManager.CreateAsync(user, request.Password);
            

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                return new RegistrationResponse() { UserId = user.Id , Token = token};
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }
                
                throw new BadRequestException($"{str}");
            }
            
        }
        
        
        public async Task<bool> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new BadRequestException($"User with id '{userId}' not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new BadRequestException($"Verification failed for user with id '{userId}'");
            }

            return true;
        }

        public async Task<ForgetPasswordResponse> ForgetPassword(string email)
        {
            // find user
            var user = await _userManager.FindByEmailAsync(email);
            // check if user is null
            if (user == null)
            {
                throw new BadRequestException($"User with email '{email}' not found.");
            }
            
            // check email comfirmed
            if (user.EmailConfirmed == false)
            {
                throw new BadRequestException($"Email {email} is not confirmed.");
            }
            
            // generate token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            return new ForgetPasswordResponse(){UserId = user.Id, Token = token};
            
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            // get user
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            // check user exist
            if (user == null)
            {
                throw new BadRequestException($"User with id '{request.UserId}' not found.");
            }
            
            // reset password
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            
            // check result
            if (!result.Succeeded)
            {
                throw new BadRequestException($"Reset password failed for user with id '{request.UserId}'");
            }

            return true;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
    
    
}
