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
    public class RedirectApiController : UmbracoAuthorizedApiController
    {
        private IRepository _redirectRepository;

        public RedirectApiController(IRepository redirectRepository)
        {
            _redirectRepository = redirectRepository;
        }


        [HttpGet]
        public IEnumerable<AllowedIp> GetAll()
        {
            return _redirectRepository.GetAll();
        }

        [HttpPost]
        public AddRedirectResponse Add(AddRedirectRequest request)
        {
            if (request == null) return new AddRedirectResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new AddRedirectResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var redirect = _redirectRepository.Create(request.ipAddress, request.Notes);
                return new AddRedirectResponse() { Success = true, NewRedirect = redirect };
            }
            catch(Exception e)
            {
                return new AddRedirectResponse() { Success = false, Message = "There was an error adding the redirect : "+ e.Message };
            }
            
        }

        [HttpPost]
        public UpdateRedirectResponse Update(UpdateRedirectRequest request)
        {

            if (request == null) return new UpdateRedirectResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new UpdateRedirectResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var redirect = _redirectRepository.Update(request.Redirect);
                return new UpdateRedirectResponse() { Success = true, UpdatedRedirect = redirect };
            }
            catch (Exception e)
            {
                return new UpdateRedirectResponse() { Success = false, Message = "There was an error updating the redirect : "+e.Message };
            }
        }

        [HttpDelete]
        public DeleteRedirectResponse Delete(int id)
        {
            if (id == 0) return new DeleteRedirectResponse() { Success = false, Message = "Invalid ID passed for redirect to delete" };

            try
            {
                _redirectRepository.Delete(id);
                return new DeleteRedirectResponse() { Success = true };
            }
            catch(Exception e)
            {
                return new DeleteRedirectResponse() { Success = false, Message = "There was an error deleting the redirect : " + e.Message };
            }
        }

        [HttpPost]
        public void ClearCache()
        {
			throw new Exception("Not sure if we should implement this");
        }
    }
}
