namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
  
    IFollowsRepository FollowsRepository { get; }
     IPostRepository PostRepository { get; }
    ICommentRepository CommentRepository { get; }
    ILikesRepository LikesRepository { get; }
    IUnlikesRepository UnlikesRepository { get; }
    INotificationRepository NotificationRepository { get; }

    IHashtagRepository HashtagRepository { get; }
  
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
  
}
