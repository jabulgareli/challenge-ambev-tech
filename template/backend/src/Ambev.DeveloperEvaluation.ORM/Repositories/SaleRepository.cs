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
    }
}
