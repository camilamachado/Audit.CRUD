﻿using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain;
using Audit.CRUD.Repository.Context;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Repository
{
	/// <summary>
	/// Repository to persist audit logs in the database.
	/// </summary>
	public class AuditLogRepository : IAuditLogRepository
	{
		private readonly AuditCRUDDbContext _context;

		public AuditLogRepository(AuditCRUDDbContext context)
		{
			_context = context;
		}

		public async Task<Result<Exception, Unit>> AddAsync(AuditLog auditLog)
		{
			var indexResponse = await _context.Database.IndexDocumentAsync(auditLog);

			if (!indexResponse.IsValid)
			{
				// TODO: validar mensagens caso dê exceção
				var debugInformation = indexResponse.DebugInformation;
				var exception = indexResponse.OriginalException;

				return exception;
			}

			return Unit.Successful;
		}
	}
}
