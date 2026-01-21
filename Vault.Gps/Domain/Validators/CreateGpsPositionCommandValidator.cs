using FluentValidation;
using vault_gps.Application.Commands;
using vault_gps.Contracts.Enums;

namespace vault_gps.Domain.Validators;

public class CreateGpsPositionCommandValidator: AbstractValidator<CreateGpsPositionCommand>
{
    public CreateGpsPositionCommandValidator()
    {
        RuleFor(command => command.AggregateId).NotEmpty().NotNull();
        RuleFor(command => command.EventType).NotEmpty().NotNull().IsEnumName(typeof(EventTypeEnum));
        RuleFor(command => command.Latitude).NotEmpty().NotNull().Matches(@"^\d+$").WithMessage("'{PropertyName}' must only contain numbers");
        RuleFor(command => command.Longitude).NotEmpty().NotNull().Matches(@"^\d+$").WithMessage("'{PropertyName}' must only contain numbers");
        RuleFor(command => command.UpdateTime).NotEmpty().NotNull();
        RuleFor(command => command.Description).NotEmpty().NotNull();
    }
}