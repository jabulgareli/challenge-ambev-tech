using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.CustomerId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.BranchId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.OwnsMany(s => s.Items, item=>
            {
                item.ToTable("SaleItems");

                item.Property(si => si.ProductId)
                    .IsRequired()
                    .HasColumnType("uuid");

                item.Property(si => si.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                item.Property(si => si.Quantity)
                    .IsRequired()
                    .HasColumnType("int");

                item.Property(si => si.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");

                item.Property(si => si.Discount)
                    .HasColumnType("decimal(18, 2)");

                item.Property(si => si.TotalAmount)
                    .HasColumnType("decimal(18, 2)");
            });
        }
    }
}
