using Our.Umbraco.IpLocker.Core.Models;
using Our.Umbraco.IpLocker.Core.Models.Pocos;
using Our.Umbraco.IpLocker.Core.Repositories;
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
        private IRepository _repository;

        public AllowedIpApiController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IEnumerable<AllowedIp> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        public AddResponse Add(AddRequest request)
        {
            if (request == null) return new AddResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new AddResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var redirect = _repository.Create(request.ipAddress, request.Notes);
                return new AddResponse() { Success = true, NewRedirect = redirect };
            }
            catch(Exception e)
            {
                return new AddResponse() { Success = false, Message = "There was an error adding the redirect : "+ e.Message };
            }
            
        }

        [HttpPost]
        public UpdateResponse Update(UpdateRequest request)
        {

            if (request == null) return new UpdateResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new UpdateResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var redirect = _repository.Update(request.Redirect);
                return new UpdateResponse() { Success = true, UpdatedRedirect = redirect };
            }
            catch (Exception e)
            {
                return new UpdateResponse() { Success = false, Message = "There was an error updating the redirect : "+e.Message };
            }
        }

        [HttpDelete]
        public DeleteResponse Delete(int id)
        {
            if (id == 0) return new DeleteResponse() { Success = false, Message = "Invalid ID passed for redirect to delete" };

            try
            {
                _repository.Delete(id);
                return new DeleteResponse() { Success = true };
            }
            catch(Exception e)
            {
                return new DeleteResponse() { Success = false, Message = "There was an error deleting the redirect : " + e.Message };
            }
        }

        [HttpPost]
        public void ClearCache()
        {
			throw new Exception("Not sure if we should implement this");
        }
    }
}
