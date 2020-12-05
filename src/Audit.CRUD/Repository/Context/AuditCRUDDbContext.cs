using Nest;

namespace Audit.CRUD.Repository.Context
{
	/// <summary>
	/// Context of the elasticsearch database
	/// </summary>
	public class AuditCRUDDbContext
	{
		public IElasticClient Database { get; private set; }

		public AuditCRUDDbContext(IElasticClient database)
		{
			Database = database;
		}
	}
}
