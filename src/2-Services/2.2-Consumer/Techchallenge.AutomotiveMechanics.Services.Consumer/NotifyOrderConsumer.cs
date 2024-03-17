using AutoMapper;
using MassTransit;
using Techchallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace Techchallenge.AutomotiveMechanics.Services.Consumer
{
    public class NotifyOrderConsumer : IConsumer<OrderEvents>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public NotifyOrderConsumer(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderEvents> context)
        {
            var message = context.Message;

            var map = _mapper.Map<Order>(message);

            await _orderService.AddAsync(map);
        }
    }
}
