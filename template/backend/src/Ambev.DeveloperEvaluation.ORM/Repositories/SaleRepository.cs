using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository(DefaultContext context) : ISaleRepository
    {
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await context.Sales.AddAsync(sale, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken)
        {
            context.Sales.Remove(sale);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Sales.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            context.Sales.Update(sale);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
