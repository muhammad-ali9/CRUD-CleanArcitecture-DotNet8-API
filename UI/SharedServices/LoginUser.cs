using Application.Interfaces;
using System.Security.Claims;

namespace UI.SharedServices
{
    public class LoginUser : IAuthenticatedUser
    {
        public LoginUser(IHttpContextAccessor httpContextAccessor)
        {
            UserLogin = httpContextAccessor.HttpContext.User.FindFirstValue("uid"); 
        }
        public string UserLogin { get; set; }
    }
}
