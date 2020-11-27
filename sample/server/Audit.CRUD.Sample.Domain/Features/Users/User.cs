namespace Audit.CRUD.Sample.Domain.Users
{
    /// <summary>
    /// Domain object that represents a user.
    /// </summary>
    public class User
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
