using System.Threading.Tasks;
using Audit.CRUD.Sample.Application.Features.Users.Handlers;
using Audit.CRUD.Sample.WebApi.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Audit.CRUD.Sample.WebApi.Features.Users
{
	[Route("[controller]")]
	public class UsersController : ApiController
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] UserCreate.Command command)
		{
			command.IpAddress = GetRemoteIpAddressIPv4();

			var result = await _mediator.Send(command);

			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(result.Success);
		}
	}
}
