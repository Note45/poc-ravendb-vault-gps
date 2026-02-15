using NSubstitute;
using vault_gps.Application.DTOs;
using vault_gps.Application.Queries;
using vault_gps.Application.QueryHandlers;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Application.QueryHandlers;

public class GetGpsAggregateByIdQueryHandlerTests
{
    [Fact(DisplayName = "Should be able to get gps aggregate by id")]
    public async Task Should_Be_Able_To_Get_Aggregate_By_Id()
    {
        // Arrange
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var handler = new GetGpsAggregateByIdQueryHandler(repoMock);
        var aggregateId = Guid.NewGuid().ToString();

        var aggregateResult = new GpsPositionAggregateResult()
        {
            AggregateId = aggregateId,
            EventType = "GpsPositionItemCreated",
            Latitude = "40",
            Longitude = "-74",
            UpdateTime = DateTime.Now,
            TotalEvents = 1
        };

        repoMock.GetAggregateById(Arg.Any<GetGpsAggregateByIdQuery>()).Returns(aggregateResult);

        var query = new GetGpsAggregateByIdQuery(aggregateId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(aggregateId, result!.AggregateId);
        Assert.Equal(aggregateResult.Latitude, result.Latitude);
        Assert.Equal(aggregateResult.Longitude, result.Longitude);
        Assert.Equal(aggregateResult.TotalEvents, result.EventCount);
        await repoMock.Received(1).GetAggregateById(Arg.Any<GetGpsAggregateByIdQuery>());
    }

    [Fact(DisplayName = "Should return null when aggregate not found")]
    public async Task Should_Return_Null_When_Aggregate_Not_Found()
    {
        // Arrange
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var handler = new GetGpsAggregateByIdQueryHandler(repoMock);

        repoMock.GetAggregateById(Arg.Any<GetGpsAggregateByIdQuery>()).Returns((GpsPositionAggregateResult?)null);

        var query = new GetGpsAggregateByIdQuery("non-existent");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Should throw exception when repository is null")]
    public void Should_Throw_Exception_When_Repository_Is_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetGpsAggregateByIdQueryHandler(null!));
    }
}

public class GetAllGpsPositionsQueryHandlerTests
{
    [Fact(DisplayName = "Should be able to get all gps positions")]
    public async Task Should_Be_Able_To_Get_All_Positions()
    {
        // Arrange
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var handler = new GetAllGpsPositionsQueryHandler(repoMock);

        var positionItems = new List<GpsPositionItem>()
        {
            new GpsPositionItem()
            {
                Id = "doc-1",
                AggregateId = Guid.NewGuid().ToString(),
                EventType = "GpsPositionItemCreated",
                Latitude = "40",
                Longitude = "-74",
                UpdateTime = DateTime.Now.ToString("O"),
                Description = "Test 1"
            },
            new GpsPositionItem()
            {
                Id = "doc-2",
                AggregateId = Guid.NewGuid().ToString(),
                EventType = "GpsPositionItemCreated",
                Latitude = "51",
                Longitude = "0",
                UpdateTime = DateTime.Now.ToString("O"),
                Description = "Test 2"
            }
        };

        repoMock.GetAllGpsPositionItems(Arg.Any<int>(), Arg.Any<int>()).Returns(positionItems);

        var query = new GetAllGpsPositionsQuery(0, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(0, result.Page);
        Assert.Equal(10, result.Size);
        await repoMock.Received(1).GetAllGpsPositionItems(Arg.Any<int>(), Arg.Any<int>());
    }

    [Fact(DisplayName = "Should throw exception when repository is null")]
    public void Should_Throw_Exception_When_Repository_Is_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetAllGpsPositionsQueryHandler(null!));
    }
}

public class GetGpsAggregatesQueryHandlerTests
{
    [Fact(DisplayName = "Should be able to get all gps aggregates")]
    public async Task Should_Be_Able_To_Get_All_Aggregates()
    {
        // Arrange
        var repoMock = Substitute.For<IGpsPositionRepository>();
        var handler = new GetGpsAggregatesQueryHandler(repoMock);

        var aggregateResults = new List<GpsPositionAggregateResult>()
        {
            new GpsPositionAggregateResult()
            {
                AggregateId = Guid.NewGuid().ToString(),
                EventType = "GpsPositionItemCreated",
                Latitude = "40",
                Longitude = "-74",
                UpdateTime = DateTime.Now,
                TotalEvents = 1
            },
            new GpsPositionAggregateResult()
            {
                AggregateId = Guid.NewGuid().ToString(),
                EventType = "GpsPositionItemCreated",
                Latitude = "51",
                Longitude = "0",
                UpdateTime = DateTime.Now,
                TotalEvents = 2
            }
        };

        repoMock.GetAllGpsPositionAggregateResults(Arg.Any<GetGpsAggregatesQuery>()).Returns(aggregateResults);

        var query = new GetGpsAggregatesQuery(0, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(0, result.Page);
        Assert.Equal(10, result.Size);
        await repoMock.Received(1).GetAllGpsPositionAggregateResults(Arg.Any<GetGpsAggregatesQuery>());
    }

    [Fact(DisplayName = "Should throw exception when repository is null")]
    public void Should_Throw_Exception_When_Repository_Is_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetGpsAggregatesQueryHandler(null!));
    }
}

