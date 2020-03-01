using Our.Umbraco.IpLocker.Core.Models.Pocos;
using System;
using System.Linq;
using Umbraco.Core.Scoping;

namespace Our.Umbraco.IpLocker.Core.Repositories
{
	public interface IStatusRepository
	{
		AllowedIpStatus GetStatus();
		AllowedIpStatus InsertStatus(AllowedIpStatus poco);
	}


	public class StatusRepository : IStatusRepository
	{
        private static IScopeProvider _scopeProvider;

        public StatusRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public AllowedIpStatus GetStatus()
        {
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				var status = db.Query<AllowedIpStatus>("SELECT * FROM AllowedIpStatus").Last();

				return status;
			}
        }

		public AllowedIpStatus InsertStatus(AllowedIpStatus poco)
		{
			var idObj = new object();
			using (var scope = _scopeProvider.CreateScope())
			{
				idObj = scope.Database.Insert(poco);
				scope.Complete();
			}

			var item = GetById(Convert.ToInt32(idObj));

			return item;
		}

		public AllowedIpStatus GetById(int id)
		{
			var query = "SELECT * FROM AllowedIpStatus WHERE Id=@0";

			using (var scope = _scopeProvider.CreateScope())
			{
				return scope.Database.FirstOrDefault<AllowedIpStatus>(query, id);
			}
		}
	}
}
