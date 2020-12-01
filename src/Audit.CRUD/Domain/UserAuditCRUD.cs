using System;

namespace Audit.CRUD.Domain
{
	public class UserAuditCRUD
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public bool NoAuthentication { get; private set; }

		public UserAuditCRUD()
		{
			NoAuthentication = true;
		}

		public UserAuditCRUD(bool noAuthentication)
		{
			if (!noAuthentication)
			{
				throw new Exception();
			}

			NoAuthentication = noAuthentication;
		}

		public UserAuditCRUD(object id)
		{
			Id = id.ToString();
		}

		public UserAuditCRUD(object id, string name)
		{
			Id = id.ToString();
			Name = name;
		}

		public UserAuditCRUD(object id, string name, string email)
		{
			Id = id.ToString();
			Name = name;
			Email = email;
		}

	}
}
