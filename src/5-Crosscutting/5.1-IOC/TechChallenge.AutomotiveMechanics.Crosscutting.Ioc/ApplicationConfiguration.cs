using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Crosscutting.Ioc
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services
                                    .AddScoped<IBaseNotification, BaseNotification>()

                                    .AddScoped<ICarRepository, CarRepository>()
                                    .AddScoped<IServiceRepository, ServiceRepository>()
                                    .AddScoped<IModelRepository, ModelRepository>()
                                    .AddScoped<IServiceCarRepository, ServiceCarRepository>()
                                    .AddScoped<IManufacturerRepository, ManufacturerRepository>();

            services
                                    .AddScoped<ICarService, CarService>()
                                    .AddScoped<IServiceService, ServiceService>()
                                    .AddScoped<IModelService, ModelService>()
                                    .AddScoped<IServiceCarService, ServiceCarService>()
                                    .AddScoped<IManufacturerService, ManufacturerService>();
            return services;
        }
    }
}