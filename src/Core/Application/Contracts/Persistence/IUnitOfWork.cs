namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
    IFollowsRepository FollowsRepository { get; }
    ICommentRepository CommentRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
