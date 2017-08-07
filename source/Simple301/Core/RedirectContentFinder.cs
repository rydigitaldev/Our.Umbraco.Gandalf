using Umbraco.Web.Routing;
using System.Linq;

namespace Simple301.Core
{
    /// <summary>
    /// Content finder to be injected at the start of the Umbraco pipeline that first
    /// looks for any redirects that path the domain (optional) the path & query
    /// </summary>
    public class RedirectContentFinder : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest request)
        {
            //Get the requested URL path + query
            var path = request.Uri.PathAndQuery.ToLower();
            var domain = request.Uri.Host.ToLower();

            //Check the table
            var matchedRedirect = RedirectRepository.FindRedirect(path, domain);
            if (matchedRedirect == null) return false;

            //Found one, set the 301 redirect on the request and return
            request.SetRedirectPermanent(matchedRedirect.NewUrl);
            return true;
        }
    }
}
