using System;

namespace Our.Umbraco.IpLocker.Core.Models.DTOs
{
	public class AllowedIpDto
	{
		public int Id { get; set; }
		public string IpAddress { get; set; }
		public DateTime LastUpdated { get; set; }
		public string Notes { get; set; }
	}
}
