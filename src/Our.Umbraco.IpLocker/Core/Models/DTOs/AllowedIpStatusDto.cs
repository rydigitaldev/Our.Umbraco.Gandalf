using System;

namespace Our.Umbraco.IpLocker.Core.Models.Pocos
{
    public class AllowedIpStatusDto
	{
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
