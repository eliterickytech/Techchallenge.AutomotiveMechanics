using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class UserResult
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
