using Audit.CRUD.Domain;
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

namespace Audit.CRUD.Sample.Application.Features.Students.Handlers
{
    public class StudentDetail
    {
        public class Query : IRequestWithResult<Student>
        {
            public int Id { get; set; }

            [JsonIgnore]
            public string IpAddress { get; set; }

            [JsonIgnore]
            public int UserId { get; set; }

            [JsonIgnore]
            public string Email { get; set; }

            [JsonIgnore]
            public string UserName { get; set; }

            public Query(int studentId, string ipAddress, int userId, string email, string userName)
            {
                Id = studentId;
                IpAddress = ipAddress;
                UserId = userId;
                Email = email;
                UserName = userName;
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
            private readonly IAuditCRUD _auditCRUD;

            public Handler(IStudentRepository studentRepository, IAuditCRUD auditCRUD)
            {
                _studentRepository = studentRepository;
                _auditCRUD = auditCRUD;
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

                await _auditCRUD.ActionRead(
                                    eventName: nameof(StudentDetail),
                                    user: new UserAuditCRUD(query.UserId, query.UserName, query.Email),
                                    location: typeof(StudentDetail).Namespace,
                                    ipAddress: query.IpAddress,
                                    currentEntity: studentCallback.Success);

                return studentCallback;
            }
        }
    }
}
