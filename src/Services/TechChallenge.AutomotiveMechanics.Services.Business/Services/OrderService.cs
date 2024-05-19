using AutoMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Crosscutting.Shared.Events;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract.Order;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBaseNotification _baseNotification;

        public OrderService(IOrderRepository orderRepository, 
            IMapper mapper, 
            IPublishEndpoint publishEndpoint, 
            IBaseNotification baseNotification)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _baseNotification = baseNotification;
        }

        public async Task NotifyOrderAsync(OrderInsertInput input)
        {
            var contract = new OrderServiceContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return;
            }

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
