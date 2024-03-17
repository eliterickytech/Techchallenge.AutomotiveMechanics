using System.Diagnostics;
using MassTransit;
using MassTransit.Metadata;
using Techchallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace Sample.Masstransit.Worker.Workers;

public class OrderInsertedConsumer : IConsumer<OrderEvents>
{
    private readonly IOrderService _orderService;

    public OrderInsertedConsumer(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Consume(ConsumeContext<OrderEvents> context)
    {
        var timer = Stopwatch.StartNew();

        var order = new Order(context.Message.VehicleName, context.Message.ServicePrice, context.Message.Email);

        await _orderService.AddAsync(order);
    }
}