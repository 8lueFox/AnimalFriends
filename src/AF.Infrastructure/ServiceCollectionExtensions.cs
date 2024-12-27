using System.Reflection;
using AF.Core.Database.Repositories;
using AF.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AF.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseEntityFramework(this IServiceCollection services,
        Action<DbContextOptionsBuilder> options)
    {
        services.AddAutoMapper(c => { }, Assembly.GetExecutingAssembly());
        
        services.AddDbContext<AfDbContext>(options);

        services.AddScoped<IAdoptionRepository, AdoptionRepository>();
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IDepartureRepository, DepartureRepository>();
        services.AddScoped<IShelterRepository, ShelterRepository>();
        services.AddScoped<IShelterUserRepository, ShelterUserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVetVisitRepository, VetVisitRepository>();
        
        return services;
    }
}