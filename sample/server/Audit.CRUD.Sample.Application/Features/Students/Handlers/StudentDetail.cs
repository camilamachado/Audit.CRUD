using Audit.CRUD.Sample.Application.Behaviors;
using Audit.CRUD.Sample.Domain.Exceptions;
using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Infra.Structs;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Application.Features.Students.Handlers
{
    public class StudentDetail
    {
        public class Query : IRequestWithResult<Student>
        {
            public int Id { get; set; }

            public Query(int studentId)
            {
                Id = studentId;
            }

            public ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            public class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
        }

        public class Handler : IRequestHandler<Query, Result<Exception, Student>>
        {
            private readonly IStudentRepository _studentRepository;

            public Handler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result<Exception, Student>> Handle(Query query, CancellationToken cancellationToken)
            {
                var studentCallback = await _studentRepository.GetByIdAsync(query.Id);
                if (studentCallback.IsFailure)
                {
                    return studentCallback.Failure;
                }

                if (studentCallback.Success is null)
                {
                    var errorNotFound = new NotFoundException($"Could not find student with id {query.Id}.");

                    return errorNotFound;
                }

                return studentCallback;
            }
        }
    }
}
