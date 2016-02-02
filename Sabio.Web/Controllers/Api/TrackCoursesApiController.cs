using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests.Track;
using Sabio.Web.Models.Requests.UsersData;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
   
    [RoutePrefix("api/TrackCourse")]
    public class TrackCoursesApiController : ApiController
    {
        private ITrackCourseService _trackCourseService;

        public TrackCoursesApiController(ITrackCourseService trackCourseService)
        {
            _trackCourseService = trackCourseService;
        }

        [Route("Get/{id:int}"), HttpGet]
        public HttpResponseMessage Track(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<TrackCourse> response = new ItemResponse<TrackCourse>();

            response.Item = _trackCourseService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("Add"),HttpPost]
        public HttpResponseMessage Insert(TrackCourseRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _trackCourseService.Insert(model);
            return Request.CreateResponse(response);
        }

        [Route("Put/{id:int}"), HttpPut]
        public HttpResponseMessage EditUserInfo(TrackCourseUpdate model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
          
            _trackCourseService.Update(model);

            return Request.CreateResponse(response);

        }

        [Route("Delete/{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _trackCourseService.Delete(id);
            return Request.CreateResponse(response);

        }

        [Route("TrackId/{id:int}"), HttpGet]
        public HttpResponseMessage TrackById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<TrackCourse> response = new ItemsResponse<TrackCourse>();
            response.Items = _trackCourseService.GetByTrack(id);
            return Request.CreateResponse(response);
        }
    }
}
