using ContractManager.Domain.Contracts;
using ContractManager.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManager.Infrastructure.Contracts.Persistence;

public class EquipmentPlacementContractConfiguration : BaseEntityTypeConfiguration<EquipmentPlacementContract>
{
    public override void Configure(EntityTypeBuilder<EquipmentPlacementContract> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.NumberOfEquipmentUnits)
            .IsRequired();
        
        builder.HasOne(c => c.ProductionFacility)
            .WithMany(pf => pf.Contracts)
            .HasForeignKey(c => c.ProductionFacilityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName($"FK_{nameof(ProductionFacility)}_{nameof(ProductionFacility.Id)}");

        builder.HasOne(c => c.Equipment)
            .WithMany(e => e.Contracts)
            .HasForeignKey(c => c.EquipmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName($"FK_{nameof(Equipment)}_{nameof(Equipment.Id)}");
    }
}