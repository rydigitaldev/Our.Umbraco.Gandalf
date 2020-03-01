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

        public AllowedIp Create(AllowedIp poco)
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

        public AllowedIp Update(AllowedIp poco)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Update(poco);
                scope.Complete();
            }
            
			return poco;
        }

        public void Delete(AllowedIp poco)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Delete(poco);
                scope.Complete();
            }           
        }

		public AllowedIp GetByIpAddress(string ipAddress)
		{
			var query = "SELECT * FROM AllowedIp WHERE ipAddress=@0";

			using (var scope = _scopeProvider.CreateScope())
			{
				return scope.Database.FirstOrDefault<AllowedIp>(query, ipAddress);
			}
		}

		public AllowedIp GetById(int id)
        {
            var query = "SELECT * FROM AllowedIp WHERE Id=@0";

			using (var scope = _scopeProvider.CreateScope())
			{
				return scope.Database.FirstOrDefault<AllowedIp>(query, id);
			}
		}
	}
}
