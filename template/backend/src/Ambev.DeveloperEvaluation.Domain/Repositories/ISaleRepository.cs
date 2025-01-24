using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken);
    }
}
