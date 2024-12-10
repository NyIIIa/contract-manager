using ContractManager.Domain.EquipmentPlacementContracts;
using ContractManager.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace ContractManager.API.Tests.Common
{
    public static class MockDatabaseInitializer
    {
        public static Mock<AppDbContext> Initialize()
        {
            var mockPublisher = new Mock<IPublisher>();
            
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ContractManager")
                .Options;
            
            var dbContext = new Mock<AppDbContext>(dbContextOptions, mockPublisher.Object);
            
            dbContext.Setup<DbSet<ProductionFacility>>(x => x.ProductionFacilities)
                .ReturnsDbSet(GetFakeProductionFacilityList());
        
            dbContext.Setup<DbSet<Equipment>>(x => x.Equipments)
                .ReturnsDbSet(GetFakeEquipmentsList());

            dbContext.Setup<DbSet<EquipmentPlacementContract>>(x => x.EquipmentPlacementContracts)
                .ReturnsDbSet(new List<EquipmentPlacementContract>());
            
            return dbContext;
        }
        
        private static List<ProductionFacility> GetFakeProductionFacilityList()
        {
            return
            [
                ProductionFacility.Create("PF001", "Assembly Plant A", 1000.50),
                ProductionFacility.Create("PF002", "Welding Facility B", 850.75),
                ProductionFacility.Create("PF003", "Machining Center C", 1200.00)
            ];
        }
    
        private static List<Equipment> GetFakeEquipmentsList()
        {
            return
            [
                Equipment.Create("EQ001", "Drill Machine", 12.53),
                Equipment.Create("EQ002", "Lathe Machine", 15.75),
                Equipment.Create("EQ003", "Welding Unit", 21.51)
            ];
        }
    }
}