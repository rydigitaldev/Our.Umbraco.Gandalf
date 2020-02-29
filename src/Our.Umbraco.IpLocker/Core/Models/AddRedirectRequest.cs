﻿
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class AddRedirectRequest
    {
        public bool IsRegex { get; set; }

        [Required]
        public string OldUrl { get; set; }
        [Required]
        public string NewUrl { get; set; }

        public string Notes { get; set; }
    }
}
