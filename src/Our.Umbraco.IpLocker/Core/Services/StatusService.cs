using Our.Umbraco.IpLocker.Core.Enums;
using Our.Umbraco.IpLocker.Core.Extensions;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
using System;

namespace Our.Umbraco.IpLocker.Core.Services
{
	public interface IStatusService
	{
		AllowedIpStatusDto GetStatus();
		AllowedIpStatusDto SetStatus(string enabled);
	}

	public class StatusService: IStatusService
	{
		private readonly IStatusRepository _statusRepository;

		public StatusService(IStatusRepository statusRepository)
		{
			_statusRepository = statusRepository;
		}

		public AllowedIpStatusDto GetStatus()
		{
			return _statusRepository.GetStatus().ToDto();
		}

		public AllowedIpStatusDto SetStatus(string status)
		{
			var poco = new AllowedIpStatus
			{
				Status = status,
				LastUpdated = DateTime.Now.ToUniversalTime()
			};

			return _statusRepository.InsertStatus(poco).ToDto();
		}
	}
}
