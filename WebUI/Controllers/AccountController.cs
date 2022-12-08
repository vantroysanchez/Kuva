using Application.Account.Command.Create;
using Application.Account.Command.Login;
using Application.Account.Command.RefreshToken;
using Application.Common.Models;
using Application.Headers.Commands.Create;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpPost("Register")]
        public async Task<string> Register(CreateAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Login")]
        public async Task<JsonResult> Login(LoginAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("RefreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<JsonResult> RefreshToken(RefreshTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
