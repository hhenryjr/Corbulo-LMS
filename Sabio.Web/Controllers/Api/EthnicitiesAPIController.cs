using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Services;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/user/ethnicities")]
    public class EthnicitiesAPIController : ApiController
    {
        private IEthnicitiesService _ethnicitiesService;

        public EthnicitiesAPIController(IEthnicitiesService EthnicitiesService)
        {
            _ethnicitiesService = EthnicitiesService;
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Ethnicities> response = new ItemsResponse<Ethnicities>();

            response.Items = _ethnicitiesService.GetAll();

            return Request.CreateResponse(response);
        }
    }
}
