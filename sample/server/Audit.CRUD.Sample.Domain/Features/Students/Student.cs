﻿namespace Audit.CRUD.Sample.Domain.Features.Students
{
	/// <summary>
	/// Domain object that represents a student.
	/// </summary>
	public class Student : Entity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public bool IsActive { get; set; }

		/// <summary>
		/// Set the student to activated.
		/// </summary>
		public void SetActivated()
		{
			IsActive = true;
		}

		/// <summary>
		/// Set the student to disabled.
		/// </summary>
		public void SetDisabled()
		{
			IsActive = false;
		}
	}
}
