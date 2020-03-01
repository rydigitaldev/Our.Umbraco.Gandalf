using Umbraco.Web.Routing;
using Our.Umbraco.IpLocker.Core.Repositories;

namespace Our.Umbraco.IpLocker.Core
{
	public class RedirectContentFinder : IContentFinder
	{
		public IRepository _repository;
		public RedirectContentFinder(IRepository repository)
		{
			_repository = repository;
		}

		public bool TryFindContent(PublishedRequest request)
		{
			// TODO: change this to be IP based - also check for ip-not-allowed
			var path = request.Uri.PathAndQuery.ToLower();

			var matchedRedirect = _repository.Get(path);
			if (matchedRedirect == null) return false;

			request.SetRedirect("/ip-not-allowed");
			
			return true;
		}
	}
}
