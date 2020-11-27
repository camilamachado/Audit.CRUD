using System;
using System.Threading.Tasks;
using Audit.CRUD.Sample.Domain.Users;
using Audit.CRUD.Sample.Infra.Structs;

namespace Audit.CRUD.Sample.Domain.Features.Users
{
	public interface IUserRepository
	{
		/// <summary>
		/// Adds the user to the database.
		/// </summary>
		/// <param name="user">User</param>
		/// <returns>Registered user</returns>
		Task<Result<Exception, User>> AddAsync(User user);

		/// <summary>
		/// Checks if the user already exists.
		/// </summary>
		/// <param name="user">User</param>
		/// <returns>True if it exists and false if it does not exist</returns>
		Task<Result<Exception, bool>> HasAnyAsync(User user);
	}
}
