using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Domain;
using Audit.CRUD.Sample.Infra.Structs;

namespace Audit.CRUD.Sample.Application.Features.Students.Handlers
{
	public class StudentCreate
	{
		public class Command : IRequest<Result<Exception, int>>
		{
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

		public class Handler : IRequestHandler<Command, Result<Exception, int>>
		{
			private readonly IStudentRepository _studentRepository;
			private readonly IMapper _mapper;
			private readonly IAuditCRUD _auditCRUD;

			public Handler(IStudentRepository studentRepository, IMapper mapper, IAuditCRUD auditCRUD)
			{
				_studentRepository = studentRepository;
				_mapper = mapper;
				_auditCRUD = auditCRUD;
			}

			public async Task<Result<Exception, int>> Handle(Command request, CancellationToken cancellationToken)
			{
				var student = _mapper.Map<Student>(request);
				student.SetActivated();

				var addStudentCallback = await _studentRepository.AddAsync(student);
				if (addStudentCallback.IsFailure)
				{
					return addStudentCallback.Failure;
				}

				var newStudent = addStudentCallback.Success;

				await _auditCRUD.ActionCreate(
										eventName: nameof(StudentCreate),
										user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
										location: typeof(StudentCreate).Namespace,
										ipAddress: request.IpAddress,
										currentEntity: newStudent);

				return newStudent.Id;
			}
		}
	}
}