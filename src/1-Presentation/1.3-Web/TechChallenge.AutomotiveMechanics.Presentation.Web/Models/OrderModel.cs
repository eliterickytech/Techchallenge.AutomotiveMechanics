using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class OrderModel : EntityModel
    {
        public string VehicleName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }
     
        public OrderModel() { }
        public OrderModel(OrderResult result)
        {
            this.Id = result.Id;
            this.Email = result.Email;
            this.VehicleName = result.VehicleName;
            this.ServicePrice = result.ServicePrice;
            this.CreatedDate = result.CreatedDate;
            this.LastModifiedDate = result.LastModifiedDate;
        }
    }
}
