using Our.Umbraco.IpLocker.Core.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class UpdateRequest

    {
        [Required]
        public AllowedIpDto Item { get; set; }
    }
}
