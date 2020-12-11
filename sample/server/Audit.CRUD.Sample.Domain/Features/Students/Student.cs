namespace Audit.CRUD.Sample.Domain.Features.Students
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
	}
}
