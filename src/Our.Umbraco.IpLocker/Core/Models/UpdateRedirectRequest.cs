using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class UpdateRedirectRequest
    {
        [Required]
        public Redirect Redirect { get; set; }
    }
}
