using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Models;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public RoleAuthorizeAttribute() { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionUserResult = context.HttpContext.Session.GetString("UserResult");
            if (string.IsNullOrEmpty(sessionUserResult))
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
                return;
            }
            var userResult = JsonConvert.DeserializeObject<UserResult>(sessionUserResult);
            if (userResult == null)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
