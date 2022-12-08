using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Command.Login
{
    public record LoginAccountCommand: IRequest<JsonResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, JsonResult>
    {
        private readonly IIdentityService _identityService;

        public LoginAccountCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<JsonResult> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.Login(request.UserName, request.Password);
        }
    }
}
