using AutoMapper;
using MassTransit;
using TechChallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;

namespace Techchallenge.AutomotiveMechanics.Services.Consumer
{
    public class NotifyOrderConsumer : IConsumer<OrderEvents>
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public NotifyOrderConsumer(IOrderService orderService, IMapper mapper, IEmailService emailService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<OrderEvents> context)
        {
            var message = context.Message;

            var map = _mapper.Map<Order>(message);

            await _orderService.AddAsync(map);
        }
    }
}
