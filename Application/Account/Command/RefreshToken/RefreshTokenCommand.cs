using Application.Account.Command.Login;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Command.RefreshToken
{
    public record RefreshTokenCommand: IRequest<JsonResult>
    {        
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, JsonResult>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public RefreshTokenCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<JsonResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string? email = _currentUserService.Email;
            return _identityService.RefreshToken(email);
        }
    }
}
