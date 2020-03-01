using Our.Umbraco.IpLocker.Core.Models;
using Our.Umbraco.IpLocker.Core.Models.DTOs;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.IpLocker.Controllers.Backoffice
{
    [PluginController("IpLocker")]
    public class AllowedIpApiController : UmbracoAuthorizedApiController
    {
        private IAllowedIpService _allowedIpService;
		private readonly IStatusService _statusService;

		public AllowedIpApiController(IAllowedIpService allowedIpService, IStatusService statusService)
        {
            _allowedIpService = allowedIpService;
			_statusService = statusService;
		}

		[HttpGet]
		public AllowedIpStatusDto GetStatus()
		{
			return _statusService.GetStatus();
		}

		[HttpGet]
		public AllowedIpStatusDto SetStatus(string status)
		{
			return _statusService.SetStatus(status);
		}


		[HttpGet]
        public IEnumerable<AllowedIpDto> GetAll()
        {
            return _allowedIpService.GetAll();
        }

        [HttpPost]
        public AddResponse Add(AddRequest request)
        {
            if (request == null) return new AddResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new AddResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var item = _allowedIpService.Create(request.ipAddress, request.Notes);
                return new AddResponse() { Success = true, Item = item };
            }
            catch(Exception e)
            {
                return new AddResponse() { Success = false, Message = "There was an error adding the item : " + e.Message };
            }
            
        }

        [HttpPost]
        public UpdateResponse Update(UpdateRequest request)
        {

            if (request == null) return new UpdateResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new UpdateResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var item = _allowedIpService.Update(request.Item);
                return new UpdateResponse() { Success = true, Item = item };
            }
            catch (Exception e)
            {
                return new UpdateResponse() { Success = false, Message = "There was an error updating the item : "+e.Message };
            }
        }

        [HttpDelete]
        public DeleteResponse Delete(int id)
        {
            if (id == 0) return new DeleteResponse() { Success = false, Message = "Invalid ID passed for item to delete" };

            try
            {
                _allowedIpService.Delete(id);
                return new DeleteResponse() { Success = true };
            }
            catch(Exception e)
            {
                return new DeleteResponse() { Success = false, Message = "There was an error deleting the item : " + e.Message };
            }
        }

        [HttpPost]
        public void ClearCache()
        {
			throw new Exception("Not sure if we should implement this");
        }
    }
}
