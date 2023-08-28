namespace Application.DTOs.Follows;

public record FollowsReturnDto(Guid UserId, Guid FollowsId, DateTime CreatedAt);