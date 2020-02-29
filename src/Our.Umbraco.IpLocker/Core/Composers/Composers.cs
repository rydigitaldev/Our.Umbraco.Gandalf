using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.IpLocker.Core.Components;

namespace Our.Umbraco.IpLocker.Core.Composers
{
    public class RedirectComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterInstance(new RedirectRepository(IScopeAccessor)).As<IRedirectRepository>();

            composition.Register(typeof(IRedirectRepository), typeof(RedirectRepository));

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
