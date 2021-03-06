﻿using System;
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
		/// Checks if there is a user with the email indicated in the database.
		/// </summary>
		/// <param name="email">Email</param>
		/// <returns>True if it exists and false if it does not exist</returns>
		Task<Result<Exception, bool>> HasAnyByEmailAsync(string email);

		/// <summary>
		/// Gets the user with the email and password specified in the database.
		/// </summary>
		/// <param name="email">Email</param>
		/// <param name="password">Password</param>
		/// <returns>User</returns>
		Task<Result<Exception, User>> GetByCredentials(string email, string password);
	}
}
