using System;

namespace Audit.CRUD.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when the object is null or empty.
	/// </summary>
	public class InvalidUserAuditCrudException : Exception
	{
		public InvalidUserAuditCrudException(string menssage) : base(menssage) { }
	}
}
