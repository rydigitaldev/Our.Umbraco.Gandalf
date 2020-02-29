using Our.Umbraco.IpLocker.Core.Models.Pocos;
using System;
using System.Collections.Generic;
using Umbraco.Core.Scoping;

namespace Our.Umbraco.IpLocker.Core.Repositories
{
	public class AllowedIpRepository : IRepository
    {
        private static IScopeProvider _scopeProvider;

        public AllowedIpRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public IEnumerable<AllowedIp> GetAll()
        {
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				var ips = db.Query<AllowedIp>("SELECT * FROM AllowedIp");

				return ips;
			}
        }

        public AllowedIp Create(string ipAddress, string notes)
        {
            var idObj = new object();
            using (var scope = _scopeProvider.CreateScope())
            {
                idObj = scope.Database.Insert(new AllowedIp()
                {
                    ipAddress = ipAddress,
                    LastUpdated = DateTime.Now.ToUniversalTime(),
                    Notes = notes
                });

                scope.Complete();                
            }

            var item = GetById(Convert.ToInt32(idObj));

            return item;
        }

        public AllowedIp Update(AllowedIp allowedIp)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
				allowedIp.LastUpdated = DateTime.Now.ToUniversalTime();
                scope.Database.Update(allowedIp);
                scope.Complete();
            }
            
			return allowedIp;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item == null) throw new ArgumentException("No allowedIp with an Id that matches " + id);

            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Delete(item);
                scope.Complete();
            }           
        }

		public AllowedIp Get(string ipAddress)
		{
			var query = "SELECT * FROM AllowedIp WHERE ipAddress=@0";

			using (var scope = _scopeProvider.CreateScope())
			{
				return scope.Database.FirstOrDefault<AllowedIp>(query, ipAddress);
			}
		}

		private AllowedIp GetById(int id)
        {
            var query = "SELECT * FROM AllowedIp WHERE Id=@0";

			using (var scope = _scopeProvider.CreateScope())
			{
				return scope.Database.FirstOrDefault<AllowedIp>(query, id);
			}
		}
	}
}
