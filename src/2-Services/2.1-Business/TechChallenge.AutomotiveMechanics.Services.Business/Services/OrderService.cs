using AutoMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techchallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task NotifyOrderAsync(OrderInsertInput input)
        {
            var map = _mapper.Map<OrderEvents>(input);

            await _publishEndpoint.Publish<OrderEvents>(map);
        }

        public async Task<OrderResult> AddAsync(Order order)
        {
            OrderResult result = null;

            var inserted = await _orderRepository.AddAsync(order);

            if (inserted > 0)
            {
                result = _mapper.Map<OrderResult>(order);
            }

            return result;
        }

        public async Task<IList<OrderResult>> ListAsync()
        {
            var result = await _orderRepository.ListAsync();

            return _mapper.Map<IList<OrderResult>>(result);
        }
    }
}
