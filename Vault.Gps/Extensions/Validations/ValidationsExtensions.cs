using FluentValidation;
using vault_gps.Domain.Validators;

namespace vault_gps.Extensions.Validations;

public static class ValidationsExtensions
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateGpsPositionCommandValidator>();
        
        return services;
    }
}