using Audit.CRUD.Domain;
using Audit.CRUD.Sample.Domain.Exceptions;
using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Infra.Structs;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Unit = Audit.CRUD.Sample.Infra.Structs.Unit;

namespace Audit.CRUD.Sample.Application.Features.Students.Handlers
{
	public class StudentDeactivate
	{
		public class Command : IRequest<Result<Exception, Unit>>
		{
			public string Reason { get; set; }

			[JsonIgnore]
			public int Id { get; set; }

			[JsonIgnore]
			public string IpAddress { get; set; }

			[JsonIgnore]
			public int UserId { get; set; }

			[JsonIgnore]
			public string Email { get; set; }

			[JsonIgnore]
			public string UserName { get; set; }

			public ValidationResult Validate()
			{
				return new Validator().Validate(this);
			}

			private class Validator : AbstractValidator<Command>
			{
				public Validator()
				{
					RuleFor(a => a.Id).NotEmpty();
					RuleFor(a => a.Reason).NotEmpty().Length(1, 100);
				}
			}
		}

		public class Handler : IRequestHandler<Command, Result<Exception, Unit>>
		{
			private readonly IStudentRepository _repository;
			private readonly IAuditCRUD _auditCRUD;

			public Handler(IStudentRepository repository, IAuditCRUD auditCRUD)
			{
				_repository = repository;
				_auditCRUD = auditCRUD;
			}

			public async Task<Result<Exception, Unit>> Handle(Command request, CancellationToken cancellationToken)
			{
				var getStudentCallback = await _repository.GetByIdAsync(request.Id);

				if (getStudentCallback.IsFailure)
					return getStudentCallback.Failure;

				if (getStudentCallback.Success is null)
				{
					var errorNotFound = new NotFoundException($"Could not find student with id {request.Id}.");

					return errorNotFound;
				}

				var oldStudent = getStudentCallback.Success.Clone<Student>();

				var currentStudent = getStudentCallback.Success;
				currentStudent.SetDisabled();

				var updatedStudentCallback = await _repository.UpdateAsync(currentStudent);
				if (updatedStudentCallback.IsFailure)
				{
					return updatedStudentCallback.Failure;
				}

				var persistAuditLog = await _auditCRUD.ActionUpdate(
														eventName: nameof(StudentDeactivate),
														user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
														location: typeof(StudentDeactivate).Namespace,
														ipAddress: request.IpAddress,
														reason: request.Reason,
														currentEntity: currentStudent,
														oldEntity: oldStudent);
				if (persistAuditLog.IsFailure)
					return persistAuditLog.Failure;

				return updatedStudentCallback.Success;
			}
		}
	}
}