using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using vault_gps.Application.Commands;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Enums;
using vault_gps.Contracts.Models;
using vault_gps.Contracts.Services;
using vault_gps.Controllers;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Controllers;

public class GpsPositionControllerTests
{
    [Fact(DisplayName = "Should be able to save the gps position item")]
    public async Task Should_Be_Able_Post_Position()
    {
        //Act
        var mockService = Substitute.For<IGpsPositionService>();
        var sut = new GpsPositionController(mockService);
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "Latitude",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now.ToShortDateString(),
            Description = "Description"
        };
        
        mockService.SaveGpsPosition(Arg.Any<GpsPositionItem>()).Returns((GpsPositionItem)command); 
        
        //Arrange
        var result = await sut.PostPosition(command) as OkObjectResult;

        //Assert
        Assert.Equivalent(command, result?.Value);
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position")]
    public async Task Should_Be_Able_To_Get_Positions_()
    {
        //Act
        var mockService = Substitute.For<IGpsPositionService>();
        var sut = new GpsPositionController(mockService);
        var positionItem = new GpsPositionItem()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "Latitue",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now.ToShortDateString(),
            Description = "Description"
        };
        var positionItemsList = new List<GpsPositionItem>()
        {
            positionItem
        };
        
        mockService.GetAllGpsPosition(Arg.Any<int>(), Arg.Any<int>()).Returns(positionItemsList);
        
        //Arrange
        var results = await sut.GetPositions(1, 10) as ObjectResult;
        
        //Assert
        Assert.Equivalent(positionItemsList, results?.Value);
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position agregates")]
    public async Task Should_Be_Able_To_Get_Positions_Agregates()
    {
        //Act
        var mockService = Substitute.For<IGpsPositionService>();
        var sut = new GpsPositionController(mockService);
        var positionItem = new GpsPositionAggregateResult()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "Latitue",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now,
        };
        var positionItemsList = new List<GpsPositionAggregateResult>()
        {
            positionItem
        };
        
        mockService.GetAllGpsPositionAggregateResults(Arg.Any<GetGpsAggregatesQuery>()).Returns(positionItemsList);
        
        //Arrange
        var results = await sut.GetAggregates(1, 10) as ObjectResult;
        
        //Assert
        Assert.Equivalent(positionItemsList, results?.Value);
    }
    
    [Fact(DisplayName = "Should be able to get gps position agregate by id")]
    public async Task Should_Be_Able_To_Get_Positions_Agregate_By_Id()
    {
        //Act
        var mockService = Substitute.For<IGpsPositionService>();
        var sut = new GpsPositionController(mockService);
        var positionItem = new GpsPositionAggregateResult()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "Latitue",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now,
        };
        
        mockService.GetAggregateById(Arg.Any<GetGpsAggregateByIdQuery>()).Returns(positionItem);
        
        //Arrange
        var results = await sut.GetAggregateById(positionItem.AggregateId) as ObjectResult;
        
        //Assert
        Assert.Equivalent(positionItem, results?.Value);
    }
}