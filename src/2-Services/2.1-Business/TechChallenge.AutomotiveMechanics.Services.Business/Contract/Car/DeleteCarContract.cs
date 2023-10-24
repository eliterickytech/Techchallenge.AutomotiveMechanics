using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car
{
    public class DeleteCarContract : BaseContract<DeleteInput>
    {
        public DeleteCarContract(DeleteInput input)
        {
            Validate(input);
        }
        protected override void Validate(DeleteInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input.Id, "Car", "Parameters is null")
                .IsGreaterThan(input.Id, 0, "Car", "The Id field must be greater than 0"));
        }
    }
}
