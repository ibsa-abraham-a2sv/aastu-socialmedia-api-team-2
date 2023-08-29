namespace Application.Models.Identity;

public class ForgetPasswordResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}