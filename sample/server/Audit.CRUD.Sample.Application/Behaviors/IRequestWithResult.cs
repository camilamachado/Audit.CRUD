using Audit.CRUD.Sample.Infra.Structs;
using MediatR;
using System;

namespace Audit.CRUD.Sample.Application.Behaviors
{
    public interface IRequestWithResult<TResponse> : IRequest<Result<Exception, TResponse>>
    {
    }
}
