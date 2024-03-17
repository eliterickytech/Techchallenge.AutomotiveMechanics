using MassTransit;
using Sample.Masstransit.Worker.Workers;
using Techchallenge.AutomotiveMechanics.Crosscutting.Shared.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, collection) =>
    {
        collection.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                x.AddConsumer<OrderInsertedConsumer>();

                cfg.Host("amqp://localhost:5672", h =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddDelayedMessageScheduler();
            });
        });
    })
    .Build();

host.Run();
