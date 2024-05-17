using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Models
{
    public class LoginViewModel
    {
        public LoginViewModel(UserResult result)
        {
            Result = result;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int Role { get; set; }
        public UserResult Result { get; }
    }
}
