using Our.Umbraco.IpLocker.Core.Models.DTOs;
using Our.Umbraco.IpLocker.Core.Models.Pocos;

namespace Our.Umbraco.IpLocker.Core.Extensions
{
    public static class AllowedIpExtensions
	{
		public static AllowedIpDto ToDto(this AllowedIp poco)
		{
			var dto = new AllowedIpDto()
			{
				Id = poco.Id,
				IpAddress = poco.IpAddress,
				Notes = poco.Notes,
				LastUpdated = poco.LastUpdated,
			};

			return dto;
		}
	}
}
