using Umbraco.Web.Routing;
using Our.Umbraco.IpLocker.Core.Repositories;

namespace Our.Umbraco.IpLocker.Core
{
	public class RedirectContentFinder : IContentFinder
	{
		public IRepository _redirectRepository;
		public RedirectContentFinder(IRepository redirectRepository)
		{
			_redirectRepository = redirectRepository;
		}

		public bool TryFindContent(PublishedRequest request)
		{
			// TODO: change this to be IP based - also check for ip-not-allowed
			var path = request.Uri.PathAndQuery.ToLower();

			var matchedRedirect = _redirectRepository.Get(path);
			if (matchedRedirect == null) return false;

			request.SetRedirect("/ip-not-allowed");
			
			return true;
		}
	}
}
