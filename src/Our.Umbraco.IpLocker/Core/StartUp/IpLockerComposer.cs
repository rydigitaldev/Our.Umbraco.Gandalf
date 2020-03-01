using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.IpLocker.Core.Repositories;
using Our.Umbraco.Simple301.Core.Services;

namespace Our.Umbraco.IpLocker.Core.StartUp
{
	public class IpLockerComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(IAllowedIpService), typeof(AllowedIpService));
			composition.Register(typeof(IRepository), typeof(AllowedIpRepository));

            composition.ContentFinders().Insert<AllowedIpContentFinder>(0);
        }
    }
}
