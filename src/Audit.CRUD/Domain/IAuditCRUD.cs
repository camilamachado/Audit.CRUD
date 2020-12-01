using Audit.CRUD.Common.Structs;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Domain
{
	public interface IAuditCRUD
	{
		Task<Result<Exception, Unit>> Create(UserAuditCRUD user, string eventName, object currentEntity, string location, string ipAddress, string reason);

		Task<Result<Exception, Unit>> Read();

		Task<Result<Exception, Unit>> Update();

		Task<Result<Exception, Unit>> Delete();
	}
}
