using System;

namespace Audit.CRUD.Sample.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when one passed credentials are nonexistent.
	/// </summary>
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException() : base("The email or password is incorrect") { }
	}
}