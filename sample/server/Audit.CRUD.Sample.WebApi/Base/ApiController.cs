using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Audit.CRUD.Sample.Infra.Structs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Audit.CRUD.Sample.WebApi.Base
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult CustomResponse<TFailure, TSuccess>(Result<TFailure, TSuccess> result)
        {
            if (IsOperationValid() && result.IsSuccess)
            {
                return Ok(result.Success);
			}
			else
			{
                AddError(result.Failure.ToString());

            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(IList<ValidationFailure> failures)
        {
            foreach (var error in failures)
            {
                AddError(error.ErrorMessage);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            }));
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }

        protected string GetRemoteIpAddressIPv4()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
            var ipAddressIPv4 = remoteIpAddress.MapToIPv4();

            return ipAddressIPv4.ToString();
        }

        protected int UserId
        {
            get { return Convert.ToInt32(GetClaimValue("UserId")); }
        }

        protected string Email
        {
            get { return string.IsNullOrWhiteSpace(GetClaimEmailAddress()) ? string.Empty : GetClaimEmailAddress(); }
        }

        protected string UserName
        {
            get { return string.IsNullOrWhiteSpace(GetClaimName()) ? string.Empty : GetClaimName(); }
        }

        private string GetClaimValue(string type)
        {
            return ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(type)?.Value;
        }

        private string GetClaimEmailAddress()
        {
            return ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.Email)?.Value;
        }

        private string GetClaimName()
        {
            return ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
