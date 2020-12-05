using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Application
{
	/// <summary>
	/// Service to create audit logs according to the action taken (CRUD).
	/// </summary>
	public class AuditCRUDService : IAuditCRUD
	{
		private readonly IAuditLogRepository _auditRepository;

		public AuditCRUDService(IAuditLogRepository auditRepository)
		{
			_auditRepository = auditRepository;
		}

		public async Task<Result<Exception, Unit>> ActionCreate(string eventName, UserAuditCRUD user, string location, string ipAddress, object currentEntity, string reason = "not informed")
		{
			var auditLog = new AuditLog(eventName: eventName,
										user: user,
										action: Actions.Create,
										location: location,
										ipAddress: ipAddress,
										reason: reason,
										currentEntity: currentEntity);

			var auditLogAddedCallback = await PersistLogInDB(auditLog);
			if (auditLogAddedCallback.IsFailure)
			{
				return auditLogAddedCallback.Failure;
			}

			return Unit.Successful;
		}

		public Task<Result<Exception, Unit>> ActionDelete()
		{
			throw new NotImplementedException();
		}

		public Task<Result<Exception, Unit>> ActionRead()
		{
			throw new NotImplementedException();
		}

		public Task<Result<Exception, Unit>> ActionUpdate()
		{
			throw new NotImplementedException();
		}

		private async Task<Result<Exception, Unit>> PersistLogInDB(AuditLog auditLog)
		{
			var auditLogIsValid = auditLog.Validate();
			if (auditLogIsValid.IsFailure)
			{
				return auditLogIsValid.Failure;
			}

			var auditLogAddedCallback = await _auditRepository.AddAsync(auditLog);
			if (auditLogAddedCallback.IsFailure)
			{
				return auditLogAddedCallback.Failure;
			}

			return Unit.Successful;
		}
	}
}
