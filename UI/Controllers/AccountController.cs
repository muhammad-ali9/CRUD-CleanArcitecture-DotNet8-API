using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]

        public async Task<IActionResult> UserLogin(LoginModel loginModel)
        {
            var result = await _accountService.UserLogin(loginModel);
            return Ok(result);
        }

        [HttpPost("UserRegistration")]
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {
            var result = await _accountService.Registration(registrationModel);
            return Ok(result);
        }
    }
}
