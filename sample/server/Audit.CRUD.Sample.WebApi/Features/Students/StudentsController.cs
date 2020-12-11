using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audit.CRUD.Sample.Application.Features.Students.Handlers;
using Audit.CRUD.Sample.Infra.Structs;
using Audit.CRUD.Sample.WebApi.Base;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unit = Audit.CRUD.Sample.Infra.Structs.Unit;

namespace Audit.CRUD.Sample.WebApi.Features.Students
{
	[Authorize]
	[Route("api/[controller]")]
	public class StudentsController : ApiController
	{
		private readonly IMediator _mediator;

		public StudentsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] StudentCreate.Command command)
		{
			var result = new Result<Exception, int>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();

			command.IpAddress = GetRemoteIpAddressIPv4();
			command.UserId = UserId;
			command.Email = Email;
			command.UserName = UserName;

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

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] StudentUpdate.Command command)
		{
			var result = new Result<Exception, Unit>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();


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

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _mediator.Send(new StudentDetail.Query(id, GetRemoteIpAddressIPv4(), UserId, Email, UserName));

			return CustomResponse(result);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _mediator.Send(new StudentDelete.Command(id));

			return CustomResponse(result);
		}
	}
}
