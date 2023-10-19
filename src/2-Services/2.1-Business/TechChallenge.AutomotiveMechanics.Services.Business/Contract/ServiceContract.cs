using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract
{
    public class ServiceContract : BaseContract<ServiceInsertInput>
    {
        public ServiceContract(ServiceInsertInput input)
        {
            Validate(input);
        }

        protected override void Validate(ServiceInsertInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Service", "Parameters is null")
                .IsNotNullOrWhiteSpace(input.Name, "Name", "The Name field cannot be empty")
                .IsGreaterThan(input.Name, 4, "Name", "The service name field must be greater than 4"));
        }
    }
}
