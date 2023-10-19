using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract
{
    public class CarContract : BaseContract<CarInsertInput>
    {
        public CarContract(CarInsertInput input)
        {
            Validate(input);
        }

        protected override void Validate(CarInsertInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Car", "Parameters is null")
                .IsNotNullOrWhiteSpace(input.Plate, "Plate", "The Plate field cannot be empty")
                .IsGreaterThan(input.ModelId, 0, "Model", "The model field must be greater than 0")
                .IsGreaterThan(input.YearManufactured, 0, "YearManufactured", "The YearManufactured field must be greater than 0"));
        }
    }
}
