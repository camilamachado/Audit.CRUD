using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audit.CRUD.Sample.Application.Features.Users.Handlers;
using Audit.CRUD.Sample.Infra.Structs;
using Audit.CRUD.Sample.WebApi.Base;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Audit.CRUD.Sample.WebApi.Features.Users
{
	[Route("api/[controller]")]
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
			var result = new Result<Exception, int>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();

			command.IpAddress = GetRemoteIpAddressIPv4();

			if (command.Validate().IsValid)
			{
				result = await _mediator.Send(command);
			}
			else
			{
				errors = command.Validate().Errors;
			}

			return command.Validate().IsValid ? CustomResponse(result) : CustomResponse(errors);
		}
	}
}
