using System.Linq;
using Raven.TestDriver;
using Raven.Client.Documents;
using NSubstitute;
using Raven.Embedded;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
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
            documentHolderMock.CreateStore().Returns(store);

            var repository = new GpsPositionRepository(documentHolderMock);

            // Act
            var result = await repository.GetAllGpsPositionItems(0, 10);

            // Assert
            Xunit.Assert.Equivalent(2,  result.Count());
        }
    }
}