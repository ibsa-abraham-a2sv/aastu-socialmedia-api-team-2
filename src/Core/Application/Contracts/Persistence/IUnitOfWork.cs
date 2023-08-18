namespace SocialSync.Core.Application.Contracts.Persistence;

public interface IUnitOfWork {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
