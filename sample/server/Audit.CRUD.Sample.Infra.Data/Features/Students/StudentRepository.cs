using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Infra.Data.Context;
using Audit.CRUD.Sample.Infra.Structs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Infra.Data.Features.Students
{
	public class StudentRepository : IStudentRepository
	{
		private AuditCRUDSampleDbContext _context;

		public StudentRepository(AuditCRUDSampleDbContext context)
		{
			_context = context;
		}

		public async Task<Result<Exception, Student>> AddAsync(Student student)
		{
			var newStudent = _context.Students.Add(student);

			await _context.SaveChangesAsync();

			return newStudent.Entity;
		}

		public Task<Result<Exception, Unit>> DeleteAsync(Student student)
		{
			_context.Students.Remove(student);

			return SaveChangesAsync();
		}

		public async Task<Result<Exception, Student>> GetByIdAsync(int studentId)
		{
			var student = await _context.Students.FirstOrDefaultAsync(a => a.Id == studentId);

			return student;
		}

		public async Task<Result<Exception, Unit>> UpdateAsync(Student student)
		{
			_context.Entry(student).State = EntityState.Modified;

			return await SaveChangesAsync();
		}

		private async Task<Result<Exception, Unit>> SaveChangesAsync()
		{
			var callback = await Result.Run(() => _context.SaveChangesAsync());

			if (callback.IsFailure)
				return callback.Failure;

			return Unit.Successful;
		}
	}
}
