using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Ambev.DeveloperEvaluation.ORM
{
    public class NoSqlContext : DbContext
    {
        public NoSqlContext(DbContextOptions<NoSqlContext> options) : base(options) { }

        public DbSet<SaleDiscountHistory> DiscountHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<SaleDiscountHistory>()
                .ToCollection("discounts_history")
                .HasKey(x => x.Id);

                 modelBuilder
                .Entity<SaleDiscountHistory>()
                .ToCollection("discounts_history")
                .HasKey(x => x.Id);
        }
    }

}
