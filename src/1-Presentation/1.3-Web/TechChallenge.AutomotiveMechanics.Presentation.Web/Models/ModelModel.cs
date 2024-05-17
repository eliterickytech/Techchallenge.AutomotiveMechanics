namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class ModelModel
    {
        public ModelModel()
        {
            Cars = new HashSet<CarModel>();
        }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public ManufacturerModel Manufacturer { get; set; }

        public ICollection<CarModel> Cars { get; set; }
    }
}
