using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using vault_gps.Application.Commands;
using vault_gps.Application.DTOs;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Enums;
using vault_gps.Controllers;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Controllers;

public class GpsPositionControllerTests
{
    [Fact(DisplayName = "Should be able to save the gps position item")]
    public async Task Should_Be_Able_Post_Position()
    {
        //Arrange
        var mockMediator = Substitute.For<IMediator>();
        var sut = new GpsPositionController(mockMediator);
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now.ToString("O"),
            Description = "Description"
        };
        
        var expectedResponse = new GpsPositionResponse()
        {
            Id = "doc-123",
            AggregateId = command.AggregateId,
            EventType = command.EventType,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            UpdateTime = command.UpdateTime,
            Description = command.Description
        };
        
        mockMediator.Send(Arg.Is<CreateGpsPositionCommand>(
            c => c.AggregateId == command.AggregateId), 
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);
        
        //Act
        var result = await sut.PostPosition(command) as CreatedAtActionResult;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(sut.GetAggregateById), result.ActionName);
        Assert.Equivalent(expectedResponse, result.Value);
        await mockMediator.Received(1).Send(Arg.Any<CreateGpsPositionCommand>(), Arg.Any<CancellationToken>());
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position")]
    public async Task Should_Be_Able_To_Get_Positions_()
    {
        //Arrange
        var mockMediator = Substitute.For<IMediator>();
        var sut = new GpsPositionController(mockMediator);
        
        var positionItem = new GpsPositionResponse()
        {
            Id = "doc-1",
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now.ToString("O"),
            Description = "Description"
        };
        
        var expectedResponse = new PaginatedResponse<GpsPositionResponse>()
        {
            Page = 1,
            Size = 10,
            TotalCount = 1,
            Items = new List<GpsPositionResponse>() { positionItem }
        };
        
        mockMediator.Send(Arg.Any<GetAllGpsPositionsQuery>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);
        
        //Act
        var result = await sut.GetPositions(1, 10) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equivalent(expectedResponse, result.Value);
        await mockMediator.Received(1).Send(Arg.Any<GetAllGpsPositionsQuery>(), Arg.Any<CancellationToken>());
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position agregates")]
    public async Task Should_Be_Able_To_Get_Positions_Agregates()
    {
        //Arrange
        var mockMediator = Substitute.For<IMediator>();
        var sut = new GpsPositionController(mockMediator);
        
        var aggregateItem = new GpsAggregateResponse()
        {
            AggregateId = Guid.NewGuid().ToString(),
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now.ToString("O"),
            Description = "GpsPositionItemCreated",
            EventCount = 1
        };
        
        var expectedResponse = new PaginatedResponse<GpsAggregateResponse>()
        {
            Page = 1,
            Size = 10,
            TotalCount = 1,
            Items = new List<GpsAggregateResponse>() { aggregateItem }
        };
        
        mockMediator.Send(Arg.Any<GetGpsAggregatesQuery>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);
        
        //Act
        var result = await sut.GetAggregates(1, 10) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equivalent(expectedResponse, result.Value);
        await mockMediator.Received(1).Send(Arg.Any<GetGpsAggregatesQuery>(), Arg.Any<CancellationToken>());
    }
    
    [Fact(DisplayName = "Should be able to get gps position agregate by id")]
    public async Task Should_Be_Able_To_Get_Positions_Agregate_By_Id()
    {
        //Arrange
        var mockMediator = Substitute.For<IMediator>();
        var sut = new GpsPositionController(mockMediator);
        var aggregateId = Guid.NewGuid().ToString();
        
        var expectedResponse = new GpsAggregateResponse()
        {
            AggregateId = aggregateId,
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now.ToString("O"),
            Description = "GpsPositionItemCreated",
            EventCount = 1
        };
        
        mockMediator.Send(Arg.Is<GetGpsAggregateByIdQuery>(
            q => q.AggregateId == aggregateId), 
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);
        
        //Act
        var result = await sut.GetAggregateById(aggregateId) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equivalent(expectedResponse, result.Value);
        await mockMediator.Received(1).Send(Arg.Any<GetGpsAggregateByIdQuery>(), Arg.Any<CancellationToken>());
    }
    
    [Fact(DisplayName = "Should return 404 when aggregate not found")]
    public async Task Should_Return_NotFound_When_Aggregate_Not_Found()
    {
        //Arrange
        var mockMediator = Substitute.For<IMediator>();
        var sut = new GpsPositionController(mockMediator);
        
        mockMediator.Send(Arg.Any<GetGpsAggregateByIdQuery>(), Arg.Any<CancellationToken>())
            .Returns((GpsAggregateResponse?)null);
        
        //Act
        var result = await sut.GetAggregateById("non-existent");
        
        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
}