using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class CarModel : EntityModel
    {
        public int ModelId { get; set; }

        public int YearManufactured { get; set; }

        public string Plate { get; set; }

        public ModelModel Model { get; set; }

        public ICollection<ServiceModel> Services { get; set; }

        public CarModel()
        {
            Services = new HashSet<ServiceModel>();
        }
        public CarModel(CarResult result)
        {
            this.Id = result.Id;
            this.YearManufactured = result.YearManufactured;
            this.Plate = result.Plate;
            this.CreatedDate = result.CreatedDate;
            this.LastModifiedDate = result.LastModifiedDate;
        }
    }
}
