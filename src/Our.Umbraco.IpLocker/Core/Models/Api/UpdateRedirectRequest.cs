using Our.Umbraco.IpLocker.Core.Models.Pocos;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class UpdateRedirectRequest
    {
        [Required]
        public AllowedIp Redirect { get; set; }
    }
}
