namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
  
    IFollowsRepository FollowsRepository { get; }
    ICommentRepository CommentRepository { get; }
    ILikesRepository LikesRepository { get; }
    IUnlikesRepository UnlikesRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
  
}
