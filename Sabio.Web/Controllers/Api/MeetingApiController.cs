using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Requests.Meeting;
using Sabio.Web.Models.Responses;
using Sabio.Web.Domain;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api.Meetings
{
    [RoutePrefix("api/meetings")]
    public class MeetingApiController : ApiController
    {
        IMeetingService _meetingsService;

        public MeetingApiController(IMeetingService meetingService)
        {
            _meetingsService = meetingService;
        }

        // GET: api/meetings
        [Route,HttpGet]
        public HttpResponseMessage Get()
        {
            ItemsResponse<Meeting> response = new ItemsResponse<Meeting>();
            response.Items = _meetingsService.GetAll()
                .OrderBy(o => o.Date).ThenBy(o => o.StartTime).ToList();
            return Request.CreateResponse(response);
        }

        // GET: api/meetings/5
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            ItemResponse<Meeting> response = new ItemResponse<Meeting>();
            response.Item = _meetingsService.GetById(id);
            return Request.CreateResponse(response);
        }

        // POST: api/meetings  
        [Route ,HttpPost]
        public HttpResponseMessage Post(MeetingAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _meetingsService.Insert(model);
            return Request.CreateResponse(response);
        }

        // PUT: api/Meetings
        [Route,HttpPut]
        public HttpResponseMessage Put(MeetingUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }       
            
            SucessResponse response = new Sabio.Web.Models.Responses.SucessResponse();
            _meetingsService.Update(model);
            return Request.CreateResponse(response);
        }

        // DELETE: api/Meetings/5
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new Sabio.Web.Models.Responses.SucessResponse();
            _meetingsService.Delete(id);
            return Request.CreateResponse(response);
        }

        // POST: api/meetings/echo
        [Route("Echo"), HttpPost]
        public HttpResponseMessage Echo(MeetingAddRequest model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        // POST: api/meetings/echovalidate
        [Route("EchoValidate"), HttpPost]
        public HttpResponseMessage EchoValidate(MeetingAddRequest model)
        {
            if (ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
