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
		/// <param name="currentEntity">Created entity</param>
		/// <param name="reason">For what reason | optional</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> ActionCreate(string eventName, UserAuditCRUD user, string location, string ipAddress, object currentEntity, string reason = "not informed");

		/// <summary>
		/// Method for creating an audit record when viewing an entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="currentEntity">Viewed entity</param>
		/// <param name="reason">For what reason | optional</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> ActionRead(string eventName, UserAuditCRUD user, string location, string ipAddress, object currentEntity, string reason = "not informed");

		/// <summary>
		/// Method for creating an audit record when updating  an entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="currentEntity">Updated entity</param>
		/// <param name="oldEntity">Entity before being updated</param>
		/// <param name="reason">For what reason | optional</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> ActionUpdate(string eventName, UserAuditCRUD user, string location, string ipAddress, object currentEntity, object oldEntity, string reason = "not informed");

		/// <summary>
		/// Method for creating an audit record when deleting an entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="oldEntity">Entity before being deleted</param>
		/// <param name="reason">For what reason | optional</param>
		/// <returns>Exception if problems persist or success</returns>
		Task<Result<Exception, Unit>> ActionDelete(string eventName, UserAuditCRUD user, string location, string ipAddress, object oldEntity, string reason = "not informed");
	}
}
