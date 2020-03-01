using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.Simple301.Core.Services
{
	public interface IAllowedIpService
	{
		AllowedIp GetById(int id);
		AllowedIp GetByIpAddress(string ipAddress);
		AllowedIp Create(string ipAddress, string notes);
		IEnumerable<AllowedIp> GetAll();
		AllowedIp Update(AllowedIp poco);
		void Delete(int id);
	}

	public class AllowedIpService : IAllowedIpService
	{
		private readonly IRepository _repository;

		public AllowedIpService(IRepository repository)
		{
			_repository = repository;
		}

		public AllowedIp Create(string ipAddress, string notes)
		{
			var poco = new AllowedIp()
			{
				IpAddress = ipAddress,
				LastUpdated = DateTime.Now.ToUniversalTime(),
				Notes = notes
			};
			return _repository.Create(poco);
		}

		public void Delete(int id)
		{
			var poco = _repository.GetById(id);
			if (poco == null)
			{
				throw new ArgumentException("No allowedIp with an Id that matches " + id);
			}

			_repository.Delete(poco);
		}

		public AllowedIp GetByIpAddress(string ipAddress)
		{
			return _repository.GetByIpAddress(ipAddress);
		}

		public IEnumerable<AllowedIp> GetAll()
		{
			return _repository.GetAll();
		}

		public AllowedIp GetById(int id)
		{
			return _repository.GetById(id);
		}

		public AllowedIp Update(AllowedIp poco)
		{
			poco.LastUpdated = DateTime.Now.ToUniversalTime();

			return _repository.Update(poco);
		}
	}
}
