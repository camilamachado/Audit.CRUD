using Audit.CRUD.Domain;
using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Infra.Structs;
using AutoMapper;
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
	public class StudentUpdate
	{
		public class Command : IRequest<Result<Exception, Unit>>
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public int Age { get; set; }

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
					RuleFor(a => a.FirstName).NotEmpty().Length(1, 50);
					RuleFor(a => a.LastName).NotEmpty().Length(1, 100);
					RuleFor(a => a.Age).NotEmpty();
				}
			}
		}

		public class Handler : IRequestHandler<Command, Result<Exception, Unit>>
		{
			private readonly IStudentRepository _repository;
			private readonly IMapper _mapper;
			private readonly IAuditCRUD _auditCRUD;

			public Handler(IStudentRepository repository, IMapper mapper, IAuditCRUD auditCRUD)
			{
				_repository = repository;
				_mapper = mapper;
				_auditCRUD = auditCRUD;
			}

			public async Task<Result<Exception, Unit>> Handle(Command request, CancellationToken cancellationToken)
			{
				var getStudentCallback = await _repository.GetByIdAsync(request.Id);

				if (getStudentCallback.IsFailure)
					return getStudentCallback.Failure;

				var oldStudent = getStudentCallback.Success.Clone<Student>();

				var currentStudent = _mapper.Map<Command, Student>(request, getStudentCallback.Success);

				var updatedStudentCallback = await _repository.UpdateAsync(currentStudent);
				if (updatedStudentCallback.IsFailure)
				{
					return updatedStudentCallback.Failure;
				}

				var persistAuditLog = await _auditCRUD.ActionUpdate(
														eventName: nameof(StudentUpdate),
														user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
														location: typeof(StudentUpdate).Namespace,
														ipAddress: request.IpAddress,
														currentEntity: currentStudent,
														oldEntity: oldStudent);
				if (persistAuditLog.IsFailure)
					return persistAuditLog.Failure;

				return updatedStudentCallback.Success;
			}
		}
	}
}