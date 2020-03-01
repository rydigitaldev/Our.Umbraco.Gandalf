using Our.Umbraco.IpLocker.Core.Models.Pocos;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class UpdateRequest

    {
        [Required]
        public AllowedIp Item { get; set; }
    }
}
