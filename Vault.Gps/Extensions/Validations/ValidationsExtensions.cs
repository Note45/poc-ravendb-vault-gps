using FluentValidation;
using FluentValidation.AspNetCore;
using vault_gps.Domain.Validators;

namespace vault_gps.Extensions.Validations;

public static class ValidationsExtensions
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<CreateGpsPositionCommandValidator>();
        });
        
        services.AddValidatorsFromAssemblyContaining(typeof(CreateGpsPositionCommandValidator));
        
        return services;
    }
}