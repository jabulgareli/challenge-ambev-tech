using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using LinqKit;
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

        public async Task<(int count, IEnumerable<Sale> sales)> SearchAsync(int page, int pageSize, Guid? customerId, Guid? branchId, string? saleNumber, CancellationToken cancellationToken)
        {
            var predicateBuilder = PredicateBuilder.New<Sale>(true);

            if (customerId.HasValue)
                predicateBuilder.And(s => s.CustomerId == customerId.Value);
            if (branchId.HasValue)
                predicateBuilder.And(s => s.BranchId == branchId.Value);
            if (!string.IsNullOrWhiteSpace(saleNumber))
                predicateBuilder.And(s => s.SaleNumber == saleNumber);

            var count = await context.Sales.CountAsync(predicateBuilder, cancellationToken);

            if (count <= 0) return (0, Enumerable.Empty<Sale>());

            var sales = await context.Sales.Where(predicateBuilder).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return (count, sales);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            context.Sales.Update(sale);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
