using ContractManager.API.Contracts;
using ContractManager.API.Tests.Common;
using ContractManager.Application.EquipmentPlacementContracts.Commands.Create;
using ContractManager.Domain.EquipmentPlacementContracts.Errors;
using ContractManager.Infrastructure.Common.Persistence;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ContractManager.API.Tests;

public class EquipmentPlacementContractControllerTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly Mock<AppDbContext> _mockDbContext;
    
    public EquipmentPlacementContractControllerTests()
    {
        _mockSender = new Mock<ISender>();
        _mockDbContext = MockDatabaseInitializer.Initialize();
    }

    [Fact]
    public async Task Should_Create_Contract_When_All_Request_Data_Is_Valid()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequest("PF001", "EQ001", 10);
        var command = new CreateEquipmentPlacementContractCommand(
            request.ProductionFacilityCode, 
            request.EquipmentCode,
            request.EquipmentQuantity
        );
        
        var commandHandler = new Mock<CreateEquipmentPlacementContractCommandHandler>(_mockDbContext.Object);
        
        _mockSender
            .Setup(x => x.Send(It.IsAny<IRequest<ErrorOr<Success>>>(),  It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var taskResult = commandHandler.Object.Handle(command, default);
                return taskResult.Result;
            });
        
        var controller = new Controllers.EquipmentPlacementContractController(_mockSender.Object);
        
        // Act
        var result = await controller.CreateEquipmentPlacementContractAsync(request);
        
        // Assert
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public async Task Should_Return_ProductionFacility_CodeNotFound_For_Invalid_ProductionFacility_Code()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequest("PF001221", "EQ002", 12);
        var command = new CreateEquipmentPlacementContractCommand(
            request.ProductionFacilityCode, 
            request.EquipmentCode,
            request.EquipmentQuantity
        );
        
        var commandHandler = new Mock<CreateEquipmentPlacementContractCommandHandler>(_mockDbContext.Object);
        
        _mockSender
            .Setup(x => x.Send(It.IsAny<IRequest<ErrorOr<Success>>>(),  It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var taskResult = commandHandler.Object.Handle(command, default);
                return taskResult.Result;
            });
        
        var controller = new Controllers.EquipmentPlacementContractController(_mockSender.Object);
        
        // Act
        var result = await controller.CreateEquipmentPlacementContractAsync(request);
        
        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        var validationProblemDetails = Assert.IsType<ValidationProblemDetails>(objectResult.Value);
        Assert.Contains(validationProblemDetails.Errors, pair => pair.Key == ProductionFacilityErrors.CodeNotFound.Code);
    }
    
    [Fact]
    public async Task Should_Return_Equipment_CodeNotFound_For_Invalid_Equipment_Code()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequest("PF001", "EQ12", 12);
        var command = new CreateEquipmentPlacementContractCommand(
            request.ProductionFacilityCode, 
            request.EquipmentCode,
            request.EquipmentQuantity
        );
        
        var commandHandler = new Mock<CreateEquipmentPlacementContractCommandHandler>(_mockDbContext.Object);
        
        _mockSender
            .Setup(x => x.Send(It.IsAny<IRequest<ErrorOr<Success>>>(),  It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var taskResult = commandHandler.Object.Handle(command, default);
                return taskResult.Result;
            });
        
        var controller = new Controllers.EquipmentPlacementContractController(_mockSender.Object);
        
        // Act
        var result = await controller.CreateEquipmentPlacementContractAsync(request);
        
        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        var validationProblemDetails = Assert.IsType<ValidationProblemDetails>(objectResult.Value);
        Assert.Contains(validationProblemDetails.Errors, pair => pair.Key == EquipmentErrors.CodeNotFound.Code);
    }

    [Fact]
    public async Task Should_Return_ProductionFacility_DeficientArea_When_ProductionFacility_Lacks_StandardArea()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequest("PF001", "EQ001", 1000);
        var command = new CreateEquipmentPlacementContractCommand(
            request.ProductionFacilityCode, 
            request.EquipmentCode,
            request.EquipmentQuantity
        );
        
        var commandHandler = new Mock<CreateEquipmentPlacementContractCommandHandler>(_mockDbContext.Object);
        
        _mockSender
            .Setup(x => x.Send(It.IsAny<IRequest<ErrorOr<Success>>>(),  It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var taskResult = commandHandler.Object.Handle(command, default);
                return taskResult.Result;
            });
        
        var controller = new Controllers.EquipmentPlacementContractController(_mockSender.Object);
        
        // Act
        var result = await controller.CreateEquipmentPlacementContractAsync(request);
        
        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        var validationProblemDetails = Assert.IsType<ValidationProblemDetails>(objectResult.Value);
        Assert.Contains(validationProblemDetails.Errors, pair => pair.Key == ProductionFacilityErrors.DeficientArea.Code);
    }
}