using FluentValidation;
using vault_gps.Contracts.Enums;
using vault_gps.Domain.Commands;

namespace vault_gps.Domain.Validators;

public class CreateGpsPositionCommandValidator: AbstractValidator<CreateGpsPositionCommand>
{
    public CreateGpsPositionCommandValidator()
    {
        RuleFor(command => command.AggregateId).NotEmpty().NotNull();
        RuleFor(command => command.EventType).NotEmpty().NotNull().IsEnumName(typeof(EventTypeEnum));
        RuleFor(command => command.Latitude).NotEmpty().NotNull();
        RuleFor(command => command.Longitude).NotEmpty().NotNull();
        RuleFor(command => command.UpdateTime).NotEmpty().NotNull();
        RuleFor(command => command.Description).NotEmpty().NotNull();
    }
}