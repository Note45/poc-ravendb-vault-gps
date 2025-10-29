using vault_gps.Contracts.Enums;
using vault_gps.Contracts.Models;
using vault_gps.Domain;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests;

public class CreateGpsPositionCommandTests
{
    [Fact(DisplayName = "Should be able to create GpsPositionItem from CreateGpsPositionCommand")]
    public void Should_Create_GpsPositionItem()
    {
        // Act
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = EventType.GpsPositionItemCreated.ToString(),
            Latitude = "1231231",
            Longitude = "5785775",
            UpdateTime = DateTime.UtcNow.ToString(),
            Description = "Home",
        }; 
        
        // Arrange
        var gpsPositionItem = (GpsPositionItem)command;

        //Assert
        Assert.Equal(command.EventType, gpsPositionItem.EventType);
        Assert.Equal(command.EventType, gpsPositionItem.EventType);
        Assert.Equal(command.Latitude, gpsPositionItem.Latitude);
        Assert.Equal(command.Longitude, gpsPositionItem.Longitude);
        Assert.Equal(command.UpdateTime, gpsPositionItem.UpdateTime);
        Assert.Equal(command.Description, gpsPositionItem.Description);
    }
}
