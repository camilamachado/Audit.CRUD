using Audit.CRUD.Common.Structs;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Domain
{
	/// <summary>
	/// Interface to create audit logs according to the action taken (CRUD).
	/// </summary>
	public interface IAuditCRUD
	{
		/// <summary>
		/// Method for creating an audit log when creating an entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="reason">For what reason</param>
		/// <param name="currentEntity">Current or updated entity</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> ActionCreate(string eventName, UserAuditCRUD user, string location, string ipAddress, string reason, object currentEntity);

		/// <summary>
		/// TODO
		/// </summary>
		Task<Result<Exception, Unit>> ActionRead();

		/// <summary>
		/// TODO
		/// </summary>
		Task<Result<Exception, Unit>> ActionUpdate();

		/// <summary>
		/// TODO
		/// </summary>
		Task<Result<Exception, Unit>> ActionDelete();
	}
}
