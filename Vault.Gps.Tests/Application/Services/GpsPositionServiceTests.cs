using NSubstitute;
using vault_gps.Application.Services;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Application.Services;

public class GpsPositionServiceTests
{
    [Fact(DisplayName = "Should be able to save the gps position item")]
    public async Task Should_Be_Able_To_Save_Position()
    {
        //Act
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var service = new GpsPositionService(repoMock);
        var positionItem = new GpsPositionItem()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = "EventType",
            Latitude = "Latitude",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now.ToShortDateString(),
            Description = "Description"
        };
        
        repoMock.SaveGpsPositionItem(Arg.Any<GpsPositionItem>()).Returns(positionItem);
        
        //Arrange
        var result = await service.SaveGpsPosition(positionItem);
        
        //Assert
        Assert.Equivalent(result, positionItem);
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position items")]
    public async Task Should_Be_Able_To_Get_All_Gps_Position_Items()
    {
        //Act
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var service = new GpsPositionService(repoMock);
        var positionItem = new GpsPositionItem()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = "EventType",
            Latitude = "Latitude",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now.ToShortDateString(),
            Description = "Description"
        };
        var positionItemsList = new List<GpsPositionItem>()
        {
            positionItem
        };
        
        repoMock.GetAllGpsPositionItems(Arg.Any<int>(), Arg.Any<int>()).Returns(positionItemsList);
        
        //Arrange
        var results = await service.GetAllGpsPosition(1, 10);
        
        //Assert
        Assert.Equivalent(results, positionItemsList);
    }
 }