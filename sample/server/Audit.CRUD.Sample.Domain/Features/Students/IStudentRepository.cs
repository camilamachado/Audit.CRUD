using Audit.CRUD.Sample.Infra.Structs;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Sample.Domain.Features.Students
{
	public interface IStudentRepository
	{
		/// <summary>
		/// Adds the student to the database.
		/// </summary>
		/// <param name="student">Student</param>
		/// <returns>Registered student</returns>
		Task<Result<Exception, Student>> AddAsync(Student student);

		/// <summary>
		/// Gets the student with the indicated Id from the database.
		/// </summary>
		/// <param name="studentId">Student id</param>
		/// <returns>Student</returns>
		Task<Result<Exception, Student>> GetByIdAsync(int studentId);

		/// <summary>
		/// Updates the student to the database.
		/// </summary>
		/// <param name="student">Student</param>
		/// <returns>Confirmation of success</returns>
		Task<Result<Exception, Unit>> UpdateAsync(Student student);

		/// <summary>
		/// Deletes the student to the database.
		/// </summary>
		/// <param name="student">Student</param>
		/// <returns>Confirmation of success</returns>
		Task<Result<Exception, Unit>> DeleteAsync(Student student);
	}
}
