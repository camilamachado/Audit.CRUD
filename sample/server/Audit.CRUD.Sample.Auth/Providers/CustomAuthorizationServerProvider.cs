using Aloji.AspNetCore.JwtSecurity.Context;
using Aloji.AspNetCore.JwtSecurity.Services.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Auth.Providers
{
    public class CustomAuthorizationServerProvider : AuthorizationServerProvider
    {
        private IMediator _mediator;

        public CustomAuthorizationServerProvider(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task GrantClientCredentialsAsync(GrantResourceOwnerCredentialsContext context)
        {
            var form = context.Request.Form;

            var clientId = form.First(c => c.Key.Equals("client_id")).Value;

            if (string.IsNullOrEmpty(clientId))
            {
                context.SetError("ErrorCode:001 - The client_id is not set");
                return;
            }

            var client = AppClientsStore.FindClient(clientId);

            if (client == null)
            {
                context.SetError("ErrorCode:002 - The client_id is incorrect");
                return;
            }

            var authVerifyCallback = await _mediator.Send(new AuthenticationUser.Query() { Email = context.UserName, Password = context.Password });

            if (authVerifyCallback.IsFailure)
            {
                context.SetError("ErrorCode:003 - Invalid user authentication");
                return;
            }

            var user = authVerifyCallback.Success;
            var claims = new List<Claim>
                        {
                            new Claim("UserId", user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim("aud", clientId),
                        };

            context.Validated(claims);
            await Task.FromResult(0);
        }
    }
}