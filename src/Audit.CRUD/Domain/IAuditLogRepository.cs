using Audit.CRUD.Common.Structs;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Domain
{
	public interface IAuditLogRepository
	{
		Task<Result<Exception, Unit>> AddAsync(AuditLog auditLog);
	}
}
