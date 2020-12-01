using Nest;
using System;

namespace Audit.CRUD.Domain
{
	public class AuditLog
	{
		/// <summary>
		/// When
		/// </summary>
		[Date(Name = "@timestamp")]
		public DateTime Timestamp { get; set; }

		/// <summary>
		/// Where
		/// </summary>
		[Text(Name = "location")]
		public string Location { get; set; }

		/// <summary>
		/// Who
		/// </summary>
		public UserAuditCRUD User { get; set; }

		/// <summary>
		/// What
		/// </summary>
		[Text(Name = "eventName")]
		public string EventName { get; set; }

		/// <summary>
		/// Why
		/// </summary>
		[Text(Name = "reason")]
		public string Reason { get; set; }

		/// <summary>
		/// Which
		/// </summary>
		[Text(Name = "ipAddress")]
		public string IpAddress { get; set; }

		/// <summary>
		/// How
		/// </summary>
		[Text(Name = "action")]
		public string Action { get; set; }

		/// <summary>
		/// Valor atual
		/// </summary>
		[Text(Name = "currentEntity")]
		public string CurrentEntity { get; set; }

		/// <summary>
		/// Valor antigo da entidade
		/// </summary>
		[Text(Name = "oldEntity")]
		public string OldEntity { get; set; }
	}
}
