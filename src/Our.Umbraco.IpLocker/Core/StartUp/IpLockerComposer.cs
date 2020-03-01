using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.IpLocker.Core.Repositories;

namespace Our.Umbraco.IpLocker.Core.StartUp
{
	public class IpLockerComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(IRepository), typeof(AllowedIpRepository));

            composition.ContentFinders().Insert<RedirectContentFinder>(0);
        }
    }
}
