using System.Linq;
using Raven.TestDriver;
using Raven.Client.Documents;
using NSubstitute;
using Raven.Embedded;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Indexes;
using vault_gps.Infra.Database.Repositories;
using Xunit;

namespace Vault.Gps.Tests.Infra.Database.Repositories;

public class GpsPositionRepositoryTests: RavenTestDriver
{
    static GpsPositionRepositoryTests()
    {
        ConfigureServer(new TestServerOptions
        {
            CommandLineArgs = new List<string>
            {
                "--License.Eula.Accepted=true"
            },
            Licensing = new ServerOptions.LicensingOptions
            {
                ThrowOnInvalidOrMissingLicense = false
            }
        });
    }
    
    [Fact(DisplayName = "Should be able to save the gps position item")]
    public async Task Should_Be_Able_To_Save_Position()
    {
        //Act
        var dockStoreMock = Substitute.For<IDocumentStoreHolder>();
        var repo = new GpsPositionRepository(dockStoreMock);
        var positionItem = new GpsPositionItem()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = "EventType",
            Latitude = "Latitude",
            Longitude = "Longitude",
            UpdateTime = DateTime.Now.ToShortDateString(),
            Description = "Description"
        };
        
        //Arrange
        var result = await repo.SaveGpsPositionItem(positionItem);
        
        //Assert
        Xunit.Assert.Equivalent(result, positionItem);
    }
    
    [Fact(DisplayName = "Should be able to get all the gps position items")]
    public async Task Should_Be_Able_To_Get_All_Gps_Position_Items()
    {
        using (var store = GetDocumentStore())
        {
            // Arrange
            using (var session = store.OpenAsyncSession())
            {
                await session.StoreAsync(new GpsPositionItem {  
                    AggregateId = Guid.NewGuid().ToString(),
                    EventType = "EventType 1",
                    Latitude = "Latitude 1",
                    Longitude = "Longitude 1",
                    UpdateTime = DateTime.Now.ToShortDateString(),
                    Description = "Description" 
                });
                await session.StoreAsync(new GpsPositionItem {    
                    AggregateId = Guid.NewGuid().ToString(),
                    EventType = "EventType 2",
                    Latitude = "Latitude 2",
                    Longitude = "Longitude 2",
                    UpdateTime = DateTime.Now.ToShortDateString(),
                    Description = "Description" 
                });
                await session.SaveChangesAsync();
            }

            WaitForIndexing(store); 

            var documentHolderMock = Substitute.For<IDocumentStoreHolder>();
            documentHolderMock.GetStore().Returns(store);

            var repository = new GpsPositionRepository(documentHolderMock);

            // Act
            var result = await repository.GetAllGpsPositionItems(0, 10);

            // Assert
            Xunit.Assert.Equivalent(2,  result.Count());
        }
    }

    [Fact(DisplayName = "Should be able to get all position items aggreate")]
    public async Task Should_Be_Able_To_Get_All_Gps_Position_Aggregate()
    {
        using (var store = GetDocumentStore())
        {
            new GpsPositionByAggregateId().Execute(store);

            using (var session = store.OpenAsyncSession())
            {
                var aggregateId = Guid.NewGuid().ToString();

                await session.StoreAsync(new GpsPositionItem
                {
                    AggregateId = aggregateId,
                    EventType = "EventType 1",
                    Latitude = "Latitude 1",
                    Longitude = "Longitude 1",
                    UpdateTime = DateTime.Now.ToShortDateString(),
                    Description = "Description"
                });

                await session.StoreAsync(new GpsPositionItem
                {
                    AggregateId = aggregateId,
                    EventType = "EventType 2",
                    Latitude = "Latitude 2",
                    Longitude = "Longitude 2",
                    UpdateTime = DateTime.Now.ToShortDateString(),
                    Description = "Description"
                });

                await session.SaveChangesAsync();
            }

            WaitForIndexing(store);

            var documentHolderMock = Substitute.For<IDocumentStoreHolder>();
            documentHolderMock.GetStore().Returns(store);

            var repository = new GpsPositionRepository(documentHolderMock);

            // Act
            var result = await repository.GetAllGpsPositionAggregateResults(new GetGpsAggregatesQuery(0, 10));

            // Assert
            Xunit.Assert.Equivalent(1, result.Count());
        }
    }
    
    [Fact(DisplayName = "Should be able to get position aggreate by id")]
    public async Task Should_Be_Able_To_Get_Position_Aggregate_By_Id()
    {
        var aggregateId = Guid.NewGuid().ToString();
        var positionItem1 = new GpsPositionItem
        {
            AggregateId = aggregateId,
            EventType = "EventType 1",
            Latitude = "Latitude 1",
            Longitude = "Longitude 1",
            UpdateTime = DateTime.Now.AddDays(0).ToShortDateString(),
            Description = "Description"
        };
        var positionItem2 = new GpsPositionItem
        {
            AggregateId = aggregateId,
            EventType = "EventType 2",
            Latitude = "Latitude 2",
            Longitude = "Longitude 2",
            UpdateTime = DateTime.Now.AddDays(2).ToShortDateString(),
            Description = "Description"
        };
        
        using (var store = GetDocumentStore())
        {
            new GpsPositionByAggregateId().Execute(store);

            using (var session = store.OpenAsyncSession())
            {
                await session.StoreAsync(positionItem1);
                await session.StoreAsync(positionItem2);
                await session.SaveChangesAsync();
            }

            WaitForIndexing(store);

            var documentHolderMock = Substitute.For<IDocumentStoreHolder>();
            documentHolderMock.GetStore().Returns(store);

            var repository = new GpsPositionRepository(documentHolderMock);
            
            // Act
            var result = await repository.GetAggregateById(new GetGpsAggregateByIdQuery(aggregateId));

            // Assert
            Xunit.Assert.Equivalent(positionItem2.AggregateId, result.AggregateId);
            Xunit.Assert.Equivalent(positionItem2.EventType, result.EventType);
            Xunit.Assert.Equivalent(positionItem2.Latitude, result.Latitude);
            Xunit.Assert.Equivalent(positionItem2.Longitude, result.Longitude);
            Xunit.Assert.Equivalent(positionItem2.UpdateTime, result.UpdateTime);
            Xunit.Assert.Equivalent(2, result.TotalEvents);
        }
    }
}