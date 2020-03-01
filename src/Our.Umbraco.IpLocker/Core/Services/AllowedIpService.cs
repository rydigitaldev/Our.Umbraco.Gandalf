using Our.Umbraco.IpLocker.Core.Extensions;
using Our.Umbraco.IpLocker.Core.Models.DTOs;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.Simple301.Core.Services
{
	public interface IAllowedIpService
	{
		AllowedIpDto GetById(int id);
		AllowedIpDto GetByIpAddress(string ipAddress);
		AllowedIpDto Create(string ipAddress, string notes);
		IEnumerable<AllowedIpDto> GetAll();
		AllowedIpDto Update(AllowedIpDto poco);
		void Delete(int id);
	}

	public class AllowedIpService : IAllowedIpService
	{
		private readonly IRepository _repository;

		public AllowedIpService(IRepository repository)
		{
			_repository = repository;
		}

		public AllowedIpDto Create(string ipAddress, string notes)
		{
			var poco = new AllowedIp()
			{
				IpAddress = ipAddress,
				LastUpdated = DateTime.Now.ToUniversalTime(),
				Notes = notes
			};

			return _repository.Create(poco).ToDto();
		}

		public void Delete(int id)
		{
			var poco = _repository.GetById(id);
			if (poco == null)
			{
				throw new ArgumentException("No item with an Id that matches " + id);
			}

			_repository.Delete(poco);
		}

		public AllowedIpDto GetByIpAddress(string ipAddress)
		{
			return _repository.GetByIpAddress(ipAddress).ToDto();
		}

		public IEnumerable<AllowedIpDto> GetAll()
		{
			return _repository.GetAll().Select(x => x.ToDto());
		}

		public AllowedIpDto GetById(int id)
		{
			return _repository.GetById(id).ToDto();
		}

		public AllowedIpDto Update(AllowedIpDto dto)
		{
			var poco = new AllowedIp()
			{
				Id = dto.Id,
				IpAddress = dto.IpAddress,
				Notes = dto.Notes,
				LastUpdated = DateTime.Now.ToUniversalTime()
			};

			return _repository.Update(poco).ToDto();
		}
	}
}
