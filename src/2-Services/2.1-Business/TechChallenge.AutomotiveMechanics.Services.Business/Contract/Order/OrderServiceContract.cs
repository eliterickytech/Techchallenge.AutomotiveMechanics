using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Order
{
    public class OrderServiceContract: BaseContract<OrderInsertInput>
    {
        public OrderServiceContract(OrderInsertInput input)
        {
            Validate(input);
        }

        protected override void Validate(OrderInsertInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Order", "Parameters is null")
                .IsNotNullOrWhiteSpace(input.VehicleName, "VehicleName", "The VehicleName field cannot be empty")
                .IsEmail(input.Email, "Email", "The Email field is invalid")
                .IsNotNull(input.ServicePrice, "ServicePrice", "The ServicePrice field canot be empty"));
        }
    }
}
