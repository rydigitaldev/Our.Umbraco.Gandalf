using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.IpLocker.Core.Repositories;

namespace Our.Umbraco.IpLocker.Core.StartUp
{
    public class RedirectComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(IRepository), typeof(AllowedIpRepository));

            composition.ContentFinders().Insert<RedirectContentFinder>(0);
        }
    }

    public class MigrationsComposer: IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components()
                 .Append<MigrationsComponent>();
        }
    }
}
