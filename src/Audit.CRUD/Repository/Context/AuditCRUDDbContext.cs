using Nest;

namespace Audit.CRUD.Repository.Context
{
	public class AuditCRUDDbContext
	{
		public IElasticClient Database { get; private set; }

		public AuditCRUDDbContext(IElasticClient database)
		{
			Database = database;
		}
	}
}
