using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Manufacturer
{
    public class DeleteManufacturerContract : BaseContract<DeleteInput>
    {
        public DeleteManufacturerContract(DeleteInput input) 
        {
            Validate(input);
        }
        protected override void Validate(DeleteInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input.Id, "Manufacturer", "Parameters is null")
                .IsGreaterThan(input.Id, 0, "Manufacturer", "The Id field must be greater than 0"));
        }
    }
}
