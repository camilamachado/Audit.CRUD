using Audit.CRUD.Sample.Domain.Exceptions;
using Audit.CRUD.Sample.Domain.Features.Users;
using Audit.CRUD.Sample.Domain.Users;
using Audit.CRUD.Sample.Infra.Data.Context;
using Audit.CRUD.Sample.Infra.Structs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Infra.Data.Features.Users
{
	public class UserRepository : IUserRepository
	{
		private AuditCRUDSampleDbContext _context;

		public UserRepository(AuditCRUDSampleDbContext context)
		{
			_context = context;
		}

		public async Task<Result<Exception, User>> AddAsync(User user)
		{
			var newUser = _context.Users.Add(user);

			await _context.SaveChangesAsync();

			return newUser.Entity;
		}

		public async Task<Result<Exception, bool>> HasAnyAsync(User user)
		{
			return await _context.Users.AnyAsync(d => d.Email == user.Email || d.Name == user.Name);
		}

		public async Task<Result<Exception, User>> GetByCredentials(string email, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password == password);

			if (user == null)
			{
				return new InvalidCredentialsException();
			}

			return user;
		}
	}
}
