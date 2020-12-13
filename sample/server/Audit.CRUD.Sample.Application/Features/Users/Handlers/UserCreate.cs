using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Audit.CRUD.Sample.Infra.Structs;
using Audit.CRUD.Sample.Domain.Features.Users;
using Audit.CRUD.Sample.Domain.Users;
using Audit.CRUD.Domain;
using System.Text.Json.Serialization;
using Audit.CRUD.Sample.Domain.Exceptions;
using Audit.CRUD.Sample.Infra.Extensions;

namespace Audit.CRUD.Sample.Application.Features.Users.Handlers
{
	public class UserCreate
	{
		public class Command : IRequest<Result<Exception, int>>
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string Password { get; set; }

			[JsonIgnore]
			public string IpAddress {get; set;}

			public ValidationResult Validate()
			{
				return new Validator().Validate(this);
			}

			private class Validator : AbstractValidator<Command>
			{
				public Validator()
				{
					RuleFor(a => a.Name).NotEmpty().Length(1, 50);
					RuleFor(a => a.Email).NotEmpty().Length(1, 100);
					RuleFor(a => a.Password).NotEmpty().Length(1, 6);
				}
			}
		}

		public class Handler : IRequestHandler<Command, Result<Exception, int>>
		{
			private readonly IUserRepository _userRepository;
			private readonly IMapper _mapper;
			private readonly IAuditCRUD _auditCRUD;

			public Handler(IUserRepository userRepository, IMapper mapper, IAuditCRUD auditCRUD)
			{
				_userRepository = userRepository;
				_mapper = mapper;
				_auditCRUD = auditCRUD;
			}

			public async Task<Result<Exception, int>> Handle(Command request, CancellationToken cancellationToken)
			{
				var hasAnyUserCallback = await _userRepository.HasAnyByEmailAsync(request.Email);
				if (hasAnyUserCallback.IsFailure)
				{
					return hasAnyUserCallback.Failure;
				}

				if (hasAnyUserCallback.Success)
				{
					return new AlreadyExistsException($"A user already exists with the email { request.Email}.");
				}

				var user = _mapper.Map<User>(request);
				user.Password = request.Password.GenerateHash();

				var addUserCallback = await _userRepository.AddAsync(user);
				if (addUserCallback.IsFailure)
				{
					return addUserCallback.Failure;
				}

				var newUser = addUserCallback.Success;

				await _auditCRUD.ActionCreate(
									eventName: nameof(UserCreate),
									user: new UserAuditCRUD(isAnonymous: true),
									location: typeof(UserCreate).Namespace,
									ipAddress: request.IpAddress,
									currentEntity: newUser);

				return newUser.Id;
			}
		}
	}
}