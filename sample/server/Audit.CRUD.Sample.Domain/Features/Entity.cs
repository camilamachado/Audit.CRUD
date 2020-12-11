namespace Audit.CRUD.Sample.Domain.Features
{
	public abstract class Entity
	{
		public virtual TEntity Clone<TEntity>()
		{
			return (TEntity)MemberwiseClone();
		}
	}
}
