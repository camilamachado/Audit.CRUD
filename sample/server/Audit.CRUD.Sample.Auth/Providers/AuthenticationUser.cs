using Audit.CRUD.Sample.Domain.Features.Users;
using Audit.CRUD.Sample.Domain.Users;
using Audit.CRUD.Sample.Infra.Extensions;
using Audit.CRUD.Sample.Infra.Structs;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Auth.Providers
{
    public class AuthenticationUser
    {
        public class Query : IRequest<Result<Exception, User>>
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            public class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(u => u.Email).NotEmpty().WithMessage("Nome de usuário não informado");

                    RuleFor(u => u.Password).NotEmpty().WithMessage("Senha não informada");
                }
            }
        }

        public class Handler : IRequestHandler<Query, Result<Exception, User>>
        {
            private IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Result<Exception, User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userRepository.GetByCredentials(request.Email, request.Password.GenerateHash());
            }

        }
    }
}