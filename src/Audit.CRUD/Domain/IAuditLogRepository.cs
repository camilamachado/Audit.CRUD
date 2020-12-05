using Audit.CRUD.Common.Structs;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Domain
{
	/// <summary>
	/// Interface to persist audit logs in the database.
	/// </summary>
	public interface IAuditLogRepository
	{
		/// <summary>
		/// Method to persist audit logs in the database.
		/// </summary>
		/// <param name="auditLog">Audit log</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> AddAsync(AuditLog auditLog);
	}
}
