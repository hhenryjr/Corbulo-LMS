using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Glimpse.Mvc.AlternateType;
using Sabio.Web.Domain;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api

{
    [RoutePrefix("api/Tracks")]
    public class TrackApiController : ApiController
    {
        private ITrackService _trackService;

        public TrackApiController(ITrackService trackService)
        {
            this._trackService = trackService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Add(TrackAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            //grabbing the ID that is currently assgined to UserService (current user)
            //this is where you get the current userId

            response.Item = _trackService.Add(model);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Edit(TrackUpdateRequest model, int id)
        {
            //the if statement is your SSV
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _trackService.Update(model);
            return Request.CreateResponse(response);

        }

        //editing 
        [Route("Get/{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<Sabio.Web.Domain.Track> response = new ItemResponse<Sabio.Web.Domain.Track>();

            response.Item = _trackService.Get(id);

            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Track> response = new ItemsResponse<Track>();

            response.Items = _trackService.GetTrackList();
            
     
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            // ItemResponse<Sabio.Web.Domain.TrackItem> response = new ItemResponse<Sabio.Web.Domain.TrackItem>();

            SucessResponse response = new SucessResponse();
            _trackService.Delete(id);

            return Request.CreateResponse(response);
        }

        [Route("courseid/{id:int}"), HttpGet]
        public HttpResponseMessage GetSection(int id)
        {

            ItemResponse<CourseSection> response = new ItemResponse<CourseSection>();

            response.Item = _trackService.GetSection(id);

            return Request.CreateResponse(response);

        }

    }

}
