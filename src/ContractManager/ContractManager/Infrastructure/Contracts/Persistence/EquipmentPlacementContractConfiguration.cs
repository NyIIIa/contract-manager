using ContractManager.Domain.Contracts;
using ContractManager.Infrastructure.Common.Persistence;
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
            .HasForeignKey(c => c.ProductionFacilityId);

        builder.HasOne(c => c.Equipment)
            .WithMany(e => e.Contracts)
            .HasForeignKey(c => c.EquipmentId);
    }
}