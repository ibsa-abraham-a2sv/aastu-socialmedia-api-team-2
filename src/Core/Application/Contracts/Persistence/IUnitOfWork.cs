namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
    IFollowsRepository FollowsRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
