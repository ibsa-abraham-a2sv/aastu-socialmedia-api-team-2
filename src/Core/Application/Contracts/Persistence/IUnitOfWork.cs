namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
    IFollowsRepository FollowsRepository { get; }
     IPostRepository PostRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
