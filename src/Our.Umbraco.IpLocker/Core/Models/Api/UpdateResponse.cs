﻿using Our.Umbraco.IpLocker.Core.Models.Pocos;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class UpdateResponse
    {
        public AllowedIp Item { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
