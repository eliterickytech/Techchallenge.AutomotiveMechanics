using System;
using MassTransit;
using TechChallenge.AutomotiveMechanics.Consumer;

async Task Main(string[] args)
{
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

    var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.Sources.Clear();
            builder.AddConfiguration(configuration);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<WorkerClient>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        // Configure as credenciais se necessário.
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);

                    cfg.ReceiveEndpoint("queue-teste", e =>
                    {
                        e.PrefetchCount = 10;
                        e.UseMessageRetry(p => p.Interval(3, 100));
                        e.ConfigureConsumer<WorkerClient>(context);
                    });
                });
            });

            //services.AddMassTransitHostedService();
        })
        .Build();

    Console.WriteLine("Waiting for new messages.");
    await host.RunAsync();
}

await Main(args);
