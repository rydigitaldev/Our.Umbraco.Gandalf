
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class AddRedirectRequest
    {
        [Required]
        public string ipAddress { get; set; }

        public string Notes { get; set; }
    }
}
