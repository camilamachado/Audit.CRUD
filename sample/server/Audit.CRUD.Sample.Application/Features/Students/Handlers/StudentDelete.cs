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

            [JsonIgnore]
            public int UserId { get; set; }

            [JsonIgnore]
            public string Email { get; set; }

            [JsonIgnore]
            public string UserName { get; set; }

            public Command(int id, string ipAddress, int userId, string email, string userName)
            {
                Id = id;
                IpAddress = ipAddress;
                UserId = userId;
                Email = email;
                UserName = userName;
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
            private readonly IAuditCRUD _auditCRUD;

            public Handler(IStudentRepository studentRepository, IAuditCRUD auditCRUD)
            {
                _studentRepository = studentRepository;
                _auditCRUD = auditCRUD;
            }

            public async Task<Result<Exception, Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var getStudentCallback = await _studentRepository.GetByIdAsync(request.Id);
                if (getStudentCallback.IsFailure)
                {
                    return getStudentCallback.Failure;
                }

                if (getStudentCallback.Success is null)
                {
                    var errorNotFound = new NotFoundException($"Could not find student with id {request.Id}.");

                    return errorNotFound;
                }

                var student = getStudentCallback.Success;

                var deleteStudentCallback = await _studentRepository.DeleteAsync(student);
                if (deleteStudentCallback.IsFailure)
                {
                    return deleteStudentCallback.Failure;
                }

                await _auditCRUD.ActionDelete(
                                    eventName: nameof(StudentDelete),
                                    user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
                                    location: typeof(StudentDelete).Namespace,
                                    ipAddress: request.IpAddress,
                                    oldEntity: student);

                return deleteStudentCallback.Success;
            }
        }
    }
}
