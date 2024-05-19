using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public record Result(HttpStatusCode StatusCode, Object Data);
    public record ResultPagination(HttpStatusCode StatusCode, Object Data, Object Pagination);
}
