using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain.Exceptions;
using System;

namespace Audit.CRUD.Domain
{
	/// <summary>
	/// Domain object that represents an user.
	/// </summary>
	public class UserAuditCRUD
	{
		public string Id { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		/// <summary>
		/// Action taken does not require authentication.
		/// When the action is not performed by an authenticated user.
		/// </summary>
		public bool NoAuthentication { get; private set; }

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		public UserAuditCRUD()
		{
			NoAuthentication = true;
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="noAuthentication">Action taken does not require authentication</param>
		public UserAuditCRUD(bool noAuthentication)
		{
			if (!noAuthentication)
			{
				throw new InvalidUserAuditCrudException("The action taken requires authentication. User data required. Pass the user data(id, name or email) by parameter.");
			}

			NoAuthentication = noAuthentication;
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="id">Unique identifier the user</param>
		public UserAuditCRUD(object id)
		{
			Id = id.ToString();
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="id">Unique identifier the user</param>
		/// <param name="name">Username</param>
		public UserAuditCRUD(object id, string name)
		{
			Id = id.ToString();
			Name = name;
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="id">Unique identifier the user</param>
		/// <param name="name">Username</param>
		/// <param name="email">User email</param>
		public UserAuditCRUD(object id, string name, string email)
		{
			Id = id.ToString();
			Name = name;
			Email = email;
		}

		/// <summary>
		/// Method responsible for validating the user entity.
		/// </summary>
		/// <returns>Exception if invalid or Successful if valid</returns>
		public Result<Exception, Unit> Validate()
		{
			if (String.IsNullOrEmpty(this.Id))
			{
				return new NullOrEmptyException("User id cannot be null or empty.");
			}

			return Unit.Successful;
		}

	}
}
