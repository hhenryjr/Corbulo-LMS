using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Requests;
using Sabio.Web.Controllers;
using Sabio.Web.Services;
using Sabio.Web.Models.Responses;
using Sabio.Web.Models.Request;

namespace Sabio.Web.Controllers.Api
{

    [RoutePrefix("Api/Events")]
    public class EventsApiController : ApiController
    {


        [Route("EchoEvents"), HttpPost]
        public HttpResponseMessage PostPerson(EventsAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = EventsService.InsertEvents(model);

            return Request.CreateResponse(model);
        }

        [Route("EchoEvents/{id:int}"), HttpPut]
        public HttpResponseMessage Update(EventsUpdateRequest model, int Id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            EventsService.Update(model);
            return Request.CreateResponse(response);

        }

    }
}
