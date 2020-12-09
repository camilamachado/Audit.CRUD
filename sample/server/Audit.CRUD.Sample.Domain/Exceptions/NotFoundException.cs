using System;

namespace Audit.CRUD.Sample.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when an object is not found.
	/// </summary>
	public class NotFoundException : Exception
	{
		public NotFoundException(string menssage) : base(menssage) { }
	}
}
