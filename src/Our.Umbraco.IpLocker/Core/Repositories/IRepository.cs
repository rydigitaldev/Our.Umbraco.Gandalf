using System.Collections.Generic;
using Our.Umbraco.IpLocker.Core.Models.Pocos;

namespace Our.Umbraco.IpLocker.Core.Repositories
{
	public interface IRepository
	{
		AllowedIp GetById(int id);
		AllowedIp GetByIpAddress(string ipAddress);
		AllowedIp Create(AllowedIp poco);
		IEnumerable<AllowedIp> GetAll();
		AllowedIp Update(AllowedIp poco);
		void Delete(AllowedIp poco);
	}
}