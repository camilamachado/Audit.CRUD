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

		/// <summary>
		/// Create a student.
		/// </summary>
		/// <param name="command">Command to create a student</param>
		/// <returns>Created student id</returns>
		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] StudentCreate.Command command)
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

		/// <summary>
		/// Update a student.
		/// </summary>
		/// <param name="id">Student id to be updated</param>
		/// <param name="command">Command to update a student</param>
		/// <returns>Confirmation of success</returns>
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] StudentUpdate.Command command)
		{
			var result = new Result<Exception, Unit>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();

			command.Id = id;
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

		/// <summary>
		/// Deactivate a student.
		/// </summary>
		/// <param name="id">Student Id to be disabled</param>
		/// <param name="command">Command to deactivate a student</param>
		/// <returns>Confirmation of success</returns>
		[HttpPatch]
		[Route("{id:int}/disabled")]
		public async Task<IActionResult> DeactivateAsync(int id, [FromBody] StudentDeactivate.Command command)
		{
			var result = new Result<Exception, Unit>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();

			command.Id = id;
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

		/// <summary>
		/// Get the student with the indicated Id.
		/// </summary>
		/// <param name="id">Student Id to be viewed</param>
		/// <returns>Student</returns>
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var result = await _mediator.Send(new StudentDetail.Query(id, GetRemoteIpAddressIPv4(), UserId, Email, UserName));

			return CustomResponse(result);
		}

		/// <summary>
		/// Delete student with the indicated Id.
		/// </summary>
		/// <param name="id">Student Id to be excluded</param>
		/// <param name="command">Command to excluded a student</param>
		/// <returns>Confirmation of success</returns>
		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteAsync(int id, [FromBody] StudentDelete.Command command)
		{
			var result = new Result<Exception, Unit>();
			IList<ValidationFailure> errors = new List<ValidationFailure>();

			command.Id = id;
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
	}
}
