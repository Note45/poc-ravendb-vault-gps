using vault_gps.Contracts.Enums;
using vault_gps.Domain.Commands;
using vault_gps.Domain.Validators;
using Xunit;
using Assert = Xunit.Assert;

namespace Vault.Gps.Tests.Domain.Validators;

public class CreateGpsPositionCommandValidatorTests
{
    [Fact(DisplayName = "Should be able to validate a correct command")]
    public void Should_Be_Able_To_Validate_Correct_Command()
    {
        //Act
        var validate = new CreateGpsPositionCommandValidator();
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = Guid.NewGuid().ToString(),
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "1231231",
            Longitude = "5785775",
            UpdateTime = DateTime.UtcNow.ToString(),
            Description = "Home",
        }; 
        
        //Arrange 
        var result = validate.Validate(command);

        //Assert    
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact(DisplayName = "Should be able to show validate command errors")]
    public void Should_Be_Able_To_Show_Validate_Command_Errors()
    {
        //Act
        var validate = new CreateGpsPositionCommandValidator();
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = "",
            EventType = "",
            Latitude = "",
            Longitude = "",
            UpdateTime = "",
            Description = "",
        }; 
        
        //Arrange 
        var result = validate.Validate(command);

        //Assert    
        Assert.False(result.IsValid);
        Assert.Equivalent(9, result.Errors.Count);
    }
    
    [Fact(DisplayName = "Should be able to show validate command errors")]
    public void Should_Be_Able_To_Show_Validate_Latitute_And_Longitude_Errors()
    {
        //Act
        var validate = new CreateGpsPositionCommandValidator();
        var command = new CreateGpsPositionCommand()
        {
            AggregateId = "12312",
            EventType = nameof(EventTypeEnum.GpsPositionItemCreated),
            Latitude = "fadfadfa",
            Longitude = "fadfadf",
            UpdateTime = DateTime.UtcNow.ToString(),
            Description = "Home",
        }; 
        
        //Arrange 
        var result = validate.Validate(command);

        //Assert    
        Assert.False(result.IsValid);
        Assert.Equivalent(2, result.Errors.Count);
        Assert.Equivalent("'Latitude' must only contain numbers", result.Errors.First().ErrorMessage);
        Assert.Equivalent("'Longitude' must only contain numbers", result.Errors.Last().ErrorMessage);
    }
}