using Our.Umbraco.IpLocker.Core.Models.Pocos;

namespace Our.Umbraco.IpLocker.Core.Models
{
    public class AddRedirectResponse
    {
        public AllowedIp NewRedirect { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
