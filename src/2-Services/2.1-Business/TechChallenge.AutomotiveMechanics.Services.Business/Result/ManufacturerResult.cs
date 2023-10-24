using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class ManufacturerResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ModelResult> Models { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

    }
}
