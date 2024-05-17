using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class ManufacturerModel : EntityModel
    {
        public string Name { get; set; }

        public ICollection<ModelModel> Models { get; set; }

        public ManufacturerModel()
        {
            Models = new HashSet<ModelModel>();
        }
        public ManufacturerModel(ManufacturerResult result)
        {
            this.Id = result.Id;
            this.Name = result.Name;
            this.CreatedDate = result.CreatedDate;
            this.LastModifiedDate = result.LastModifiedDate;
            this.Models = result.Models.Select(x => new ModelModel
            {
                Name = x.Name
            }).ToList();
        }
    }
}
