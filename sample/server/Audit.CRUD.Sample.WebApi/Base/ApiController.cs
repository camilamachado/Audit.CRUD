﻿using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Audit.CRUD.Sample.WebApi.Base
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
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
    }
}
