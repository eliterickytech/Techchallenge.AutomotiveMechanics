namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class ServiceModel : EntityModel
    {
        public string Name { get; set; }

        public int CarId { get; set; }

        public CarModel Car { get; set; }
    }
}
