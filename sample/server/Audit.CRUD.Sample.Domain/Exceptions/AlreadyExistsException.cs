using System;

namespace Audit.CRUD.Sample.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when the object already exists in the database.
	/// </summary>
	public class AlreadyExistsException : Exception
	{
		public AlreadyExistsException(string message) : base(message) { }
	}
}