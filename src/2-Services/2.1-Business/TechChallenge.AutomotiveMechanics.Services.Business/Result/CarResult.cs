using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class CarResult : ICloneable
    {
        public int Id { get; set; }

        public int YearManufactured { get; set; }

        public string Plate { get; set; }   

        public DateTime CreatedDate { get; set; }   

        public DateTime? LastModifiedDate { get; set; }

        public object Clone()
        {
            var car = (CarResult)MemberwiseClone();
            return car;
        }

        public CarResult TypedClone()
        {
            return(CarResult)Clone();
        }
    }
}
