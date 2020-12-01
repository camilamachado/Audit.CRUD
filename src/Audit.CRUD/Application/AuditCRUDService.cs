using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Audit.CRUD.Application
{
	public class AuditCRUDService : IAuditCRUD
	{
		private readonly IAuditLogRepository _auditRepository;

		public AuditCRUDService(IAuditLogRepository auditRepository)
		{
			_auditRepository = auditRepository;
		}

		public async Task<Result<Exception, Unit>> Create(UserAuditCRUD user, string eventName, object currentEntity, string location, string ipAddress, string reason)
		{
			var auditLog = new AuditLog();
			auditLog.Timestamp = DateTime.Now;
			auditLog.User = user;
			auditLog.EventName = eventName;
			auditLog.CurrentEntity = JsonConvert.SerializeObject(currentEntity); ;
			auditLog.Location = location;
			auditLog.IpAddress = ipAddress;
			auditLog.Reason = reason;
			auditLog.Action = Actions.Create;

			var addCb = await _auditRepository.AddAsync(auditLog);

			return Unit.Successful;
		}

		public Task<Result<Exception, Unit>> Delete()
		{
			throw new NotImplementedException();
		}

		public Task<Result<Exception, Unit>> Read()
		{
			throw new NotImplementedException();
		}

		public Task<Result<Exception, Unit>> Update()
		{
			throw new NotImplementedException();
		}
	}
}
