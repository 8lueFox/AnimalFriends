using System.Reflection;
using AF.Core.Behaviors;
using AF.Core.Services;
using AF.Core.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AF.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<ICryptoProvider, AesCryptoProvider>();
        
        services.AddMediatR(x => { x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}