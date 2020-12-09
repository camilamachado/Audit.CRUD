using Audit.CRUD.Sample.Application.Behaviors;
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
    public class StudentDelete
    {
        public class Command : IRequestWithResult<Unit>
        {
            public int Id { get; set; }

            [JsonIgnore]
            public string IpAddress { get; set; }

            public Command(int id)
            {
                Id = id;
            }

            public ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            public class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }

		public class Handler : IRequestHandler<Command, Result<Exception, Unit>>
        {
            private readonly IStudentRepository _studentRepository;

            public Handler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result<Exception, Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var studentCallback = await _studentRepository.GetByIdAsync(request.Id);
                if (studentCallback.IsFailure)
                {
                    return studentCallback.Failure;
                }

                if (studentCallback.Success is null)
                {
                    var errorNotFound = new NotFoundException($"Could not find student with id {request.Id}.");

                    return errorNotFound;
                }

                var student = studentCallback.Success;

                var removeCallback = await _studentRepository.DeleteAsync(student);
                if (removeCallback.IsFailure)
                {
                    return removeCallback.Failure;
                }

                return removeCallback.Success;
            }
        }
    }
}
