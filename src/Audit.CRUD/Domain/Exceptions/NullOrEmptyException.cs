using System;

namespace Audit.CRUD.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when UserAuditCRUD is invalid.
	/// </summary>
	public class NullOrEmptyException : Exception
	{
		public NullOrEmptyException(string menssage) : base(menssage) { }
	}
}
