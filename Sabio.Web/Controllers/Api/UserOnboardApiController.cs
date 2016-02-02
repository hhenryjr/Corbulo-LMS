using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UserOnboard;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;


namespace Sabio.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/user/onboard")]
    public class UserOnboardController : ApiController
    {
        private IUserOnboardService _UserOnboardService;
        
        public UserOnboardController(IUserOnboardService UserOnboardService)
        {
            this._UserOnboardService = UserOnboardService;
        }

        [Route("{id:int}"), HttpGet]    
        public HttpResponseMessage Get(int id)
        {
            ItemResponse<OnboardRegistration> response = new ItemResponse<OnboardRegistration>();
            response.Item = _UserOnboardService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("tab1/{id:int}"), HttpPut]
        public HttpResponseMessage Update1(UserOnboardUpdate1 model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _UserOnboardService.Update1(model);
            return Request.CreateResponse(response);
        }

        [Route("tab2/{id:int}"), HttpPut]
        public HttpResponseMessage Update2(UserOnboardUpdate2 model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _UserOnboardService.Update2(model);
            return Request.CreateResponse(response);
        }

        [Route("tab3/{id:int}"), HttpPut]
        public HttpResponseMessage Update3(UserOnboardUpdate3 model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _UserOnboardService.Update3(model);
            return Request.CreateResponse(response);
        }

        [Route("tab4/{id:int}"), HttpPut]
        public HttpResponseMessage Update4(UserOnboardUpdate4 model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _UserOnboardService.Update4(model);
            return Request.CreateResponse(response);
        }

        [Route("tab5/{id:int}"), HttpPut]
        public HttpResponseMessage Update5(UserOnboardUpdate5 model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _UserOnboardService.Update5(model);
            return Request.CreateResponse(response);
        }
    }
}
