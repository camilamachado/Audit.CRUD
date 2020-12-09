using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Infra.Structs;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
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

			public Handler(IStudentRepository repository, IMapper mapper)
			{
				_repository = repository;
				_mapper = mapper;
			}

			public async Task<Result<Exception, Unit>> Handle(Command request, CancellationToken cancellationToken)
			{
				var studentCallback = await _repository.GetByIdAsync(request.Id);

				if (studentCallback.IsFailure)
					return studentCallback.Failure;

				var student = studentCallback.Success;

				var studentUpdated = _mapper.Map<Command, Student>(request, student);

				return await _repository.UpdateAsync(studentUpdated);
			}
		}
	}
}