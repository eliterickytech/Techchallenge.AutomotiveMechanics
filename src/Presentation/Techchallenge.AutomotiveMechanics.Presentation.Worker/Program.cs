using MassTransit;
using TechChallenge.AutomotiveMechanics.Presentation.Worker;
using TechChallenge.AutomotiveMechanics.Presentation.Worker.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using AutoMapper;
using TechChallenge.AutomotiveMechanics.Services.Business.Profile;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<NotifyOrderConsumer>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IBaseNotification, BaseNotification>();
        AutoMapperConfig.ConfigureMappings(services);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("AutomotiveMechanics"));
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("amqp://localhost:5672", h =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });
        });
    })
    .Build();

host.Run();
public static class AutoMapperConfig
{
    public static void ConfigureMappings(IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ProfileMapping());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
