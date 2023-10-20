using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class ServiceResult
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CarId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        //public ICollection<CarResult> Cars { get; set; } = new HashSet<CarResult>();
        public CarResult Cars { get; set; } = new CarResult();
    }
}
