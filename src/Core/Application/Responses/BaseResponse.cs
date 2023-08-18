namespace SocialSync.Core.Application.Responses;

public class BaseCommandResponse {
    public int Id { get; private set; }
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public List<string>? Errors { get; private set; }
}
