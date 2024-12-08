using ContractManager.Domain.Contracts;
using ContractManager.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManager.Infrastructure.Contracts.Persistence;

public class EquipmentConfiguration : BaseEntityTypeConfiguration<Equipment>
{
    public override void Configure(EntityTypeBuilder<Equipment> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);
        
        builder.Property(e => e.Area)
            .IsRequired()
            .HasPrecision(8, 2);
        
        builder.HasIndex(e => e.Code)
            .IsUnique();
    }
}