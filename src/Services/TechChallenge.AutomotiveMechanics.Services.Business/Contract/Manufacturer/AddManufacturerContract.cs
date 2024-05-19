using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Manufacturer
{
    public class AddManufacturerContract : BaseContract<ManufacturerInsertInput>
    {
        public AddManufacturerContract(ManufacturerInsertInput input)
        {
            Validate(input);
        }
        protected override void Validate(ManufacturerInsertInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Manufacturer", "Parameters is null")
                .IsNotNullOrWhiteSpace(input.Name, "Name", "The Name field cannot be empty")
                .IsGreaterThan(input.Name, 2, "Name", "The Name field must be greater than 2"));
        }
    }
}
