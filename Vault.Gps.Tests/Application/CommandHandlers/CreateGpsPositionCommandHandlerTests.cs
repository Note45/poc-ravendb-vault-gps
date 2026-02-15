using NSubstitute;
using vault_gps.Application.CommandHandlers;
using vault_gps.Application.Commands;
using vault_gps.Application.DTOs;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Application.CommandHandlers;

public class CreateGpsPositionCommandHandlerTests
{
    [Fact(DisplayName = "Should be able to save the gps position item")]
    public async Task Should_Be_Able_To_Save_Position()
    {
        // Arrange
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var handler = new CreateGpsPositionCommandHandler(repoMock);
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = "GpsPositionItemCreated",
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now.ToString("O"),
            Description = "New York"
        };

        var savedItem = new GpsPositionItem()
        {
            Id = "doc-123",
            AggregateId = command.AggregateId,
            EventType = command.EventType,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            UpdateTime = command.UpdateTime,
            Description = command.Description
        };

        repoMock.SaveGpsPositionItem(Arg.Any<GpsPositionItem>()).Returns(savedItem);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(savedItem.Id, result.Id);
        Assert.Equal(command.AggregateId, result.AggregateId);
        Assert.Equal(command.Latitude, result.Latitude);
        Assert.Equal(command.Longitude, result.Longitude);
        await repoMock.Received(1).SaveGpsPositionItem(Arg.Any<GpsPositionItem>());
    }

    [Fact(DisplayName = "Should throw exception when repository is null")]
    public void Should_Throw_Exception_When_Repository_Is_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateGpsPositionCommandHandler(null!));
    }
}

