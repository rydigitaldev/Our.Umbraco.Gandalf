using System.Collections.Generic;
using Our.Umbraco.IpLocker.Core.Models.Pocos;

namespace Our.Umbraco.IpLocker.Core.Repositories
{
    public interface IRepository
    {
		AllowedIp Get(string ipAddress);
		AllowedIp Create(string ipAddress, string notes);
        IEnumerable<AllowedIp> GetAll();
        AllowedIp Update(AllowedIp redirect);
		void Delete(int id);
	}
}