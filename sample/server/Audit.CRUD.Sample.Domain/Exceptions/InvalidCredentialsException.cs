using System;

namespace Audit.CRUD.Sample.Domain.Exceptions
{
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException() : base("The email or password is incorrect") { }
	}
}