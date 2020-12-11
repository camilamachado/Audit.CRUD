using Audit.CRUD.Common.Structs;
using Audit.CRUD.Domain.Exceptions;
using Nest;
using Newtonsoft.Json;
using System;

namespace Audit.CRUD.Domain
{
	/// <summary>
	/// Domain object that represents an audit log.
	/// </summary>
	public class AuditLog
	{
		/// <summary>
		/// When - Provenance W7 | Quando
		/// EX: occurred in 6/19/2015 10:35:50 AM
		/// </summary>
		[Date(Name = "@timestamp")]
		public DateTime Timestamp { get; private set; }

		/// <summary>
		/// What - Provenance W7 | O que
		/// EX: what happened <create student>
		/// EX: what happened <update student>
		/// EX: what happened <disable student>
		/// </summary>
		[Text(Name = "eventName")]
		public string EventName { get; private set; }

		/// <summary>
		/// Who - Provenance W7 | Quem
		/// EX: who caused: Jayme
		/// </summary>
		public UserAuditCRUD User { get; private set; }

		/// <summary>
		/// How - Provenance W7 | Como
		/// EX: led to <create>
		/// EX: led to <read>
		/// EX: led to <update>
		/// EX: led to <delete>
		/// </summary>
		[Text(Name = "action")]
		public string Action { get; private set; }

		/// <summary>
		/// Where - Provenance W7 | Onde
		/// EX: happened in <namespace>
		/// EX: happened in <microservice>
		/// </summary>
		[Text(Name = "location")]
		public string Location { get; private set; }

		/// <summary>
		/// Which - Provenance W7 | Qual
		/// EX: was used by 127.0.0.1
		/// </summary>
		[Text(Name = "ipAddress")]
		public string IpAddress { get; private set; }

		/// <summary>
		/// Why - Provenance W7 - Porque
		/// EX: is because completed the course
		/// </summary>
		[Text(Name = "reason")]
		public string Reason { get; private set; }

		/// <summary>
		/// Current entity
		/// </summary>
		[Text(Name = "currentEntity")]
		public string CurrentEntity { get; private set; }

		/// <summary>
		/// Old entity
		/// </summary>
		[Text(Name = "oldEntity")]
		public string OldEntity { get; private set; }

		/// <summary>
		/// Constructor of audit log entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="action">Action taken</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="reason">For what reason</param>
		/// <param name="currentEntity">Current or updated entity</param>
		public AuditLog(string eventName, UserAuditCRUD user, string action, string location, string ipAddress, string reason, object currentEntity)
		{
			this.Timestamp = DateTime.Now;
			this.EventName = eventName;
			this.User = user;
			this.Action = action;
			this.Location = location;
			this.IpAddress = ipAddress;
			this.Reason = reason;
			this.CurrentEntity = JsonConvert.SerializeObject(currentEntity);
		}

		/// <summary>
		/// Constructor of audit log entity.
		/// </summary>
		/// <param name="eventName">Name of the started event</param>
		/// <param name="user">UserAuditCRUD entity</param>
		/// <param name="action">Action taken</param>
		/// <param name="location">Local where the action took place</param>
		/// <param name="ipAddress">Which device performed the action</param>
		/// <param name="reason">For what reason</param>
		/// <param name="currentEntity">Current or updated entity</param>
		/// <param name="oldEntity">Entity before performing the action</param>
		public AuditLog(string eventName, UserAuditCRUD user, string action, string location, string ipAddress, string reason, object currentEntity, object oldEntity)
		{
			this.Timestamp = DateTime.Now;
			this.EventName = eventName;
			this.User = user;
			this.Action = action;
			this.Location = location;
			this.IpAddress = ipAddress;
			this.Reason = reason;
			this.CurrentEntity = JsonConvert.SerializeObject(currentEntity);
			this.OldEntity = JsonConvert.SerializeObject(oldEntity);
		}

		/// <summary>
		/// Method responsible for validating the audit log entity.
		/// </summary>
		/// <returns>Exception if invalid or Successful if valid</returns>
		public Result<Exception, Unit> Validate()
		{
			if (String.IsNullOrEmpty(this.EventName))
			{
				return new NullOrEmptyException("Event name cannot be null or empty.");
			}

			this.User.Validate();

			if (String.IsNullOrEmpty(this.Action))
			{
				return new NullOrEmptyException("Event name cannot be null or empty.");
			}

			if (String.IsNullOrEmpty(this.Location))
			{
				return new NullOrEmptyException("Location cannot be null or empty.");
			}

			if (String.IsNullOrEmpty(this.CurrentEntity))
			{
				return new NullOrEmptyException("Current entity cannot be null or empty.");
			}

			return Unit.Successful;
		}
	}
}
