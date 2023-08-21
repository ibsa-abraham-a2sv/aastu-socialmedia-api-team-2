namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable {
    IFollowsRepository FollowsRepository { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
