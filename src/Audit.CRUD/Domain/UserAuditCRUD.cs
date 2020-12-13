using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain.Exceptions;
using System;

namespace Audit.CRUD
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
		/// The action performed is done by an unauthenticated user.
		/// When the action is not performed by an authenticated user.
		/// </summary>
		public bool IsAnonymous { get; private set; }

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		public UserAuditCRUD()
		{
			IsAnonymous = true;
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="isAnonymous">The action performed is done by an unauthenticated user</param>
		public UserAuditCRUD(bool isAnonymous)
		{
			if (!isAnonymous)
			{
				throw new InvalidUserAuditCrudException("The action taken requires authentication. User data is required. Pass user data (id, name or email) by parameter.");
			}

			IsAnonymous = isAnonymous;
		}

		/// <summary>
		/// Constructor of user entity.
		/// </summary>
		/// <param name="id">Unique identifier the user</param>
		public UserAuditCRUD(object id)
		{
			Id = id.ToString();
			IsAnonymous = false;
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
