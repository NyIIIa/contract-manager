using ContractManager.Domain.Contracts;
using ContractManager.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManager.Infrastructure.Contracts.Persistence;

public class ProductionFacilityConfiguration : BaseEntityTypeConfiguration<ProductionFacility>
{
    public override void Configure(EntityTypeBuilder<ProductionFacility> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(e => e.StandardArea)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasIndex(e => e.Code)
            .IsUnique();
    }
}