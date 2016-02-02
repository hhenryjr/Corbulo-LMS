using Microsoft.Practices.Unity;
//using Sabio.Web.Controllers.Attributes;
using Sabio.Web.Domain;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.CoursesRequest;
using Sabio.Web.Models.Requests.Tests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/courses")]
    public class CoursesAPIController : ApiController
    {
        private ICoursesService _coursesService;
        //private IUserService _userService;

        public CoursesAPIController(ICoursesService CoursesService/*, IUserService UserService*/)
        {
            _coursesService = CoursesService;
            //_userService = UserService;
        }

        [Route, HttpPost]
        public HttpResponseMessage CoursesValidation(AddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            string userId = UserService.GetCurrentUserId();
            response.Item = _coursesService.Add(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage EditCourses(CourseUpdateRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _coursesService.Update(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            ItemResponse<Course> response = new ItemResponse<Course>();

            response.Item = _coursesService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Course> response = new ItemsResponse<Course>();
            response.Items = _coursesService.Get();
            return Request.CreateResponse(response);
        }

        [Route("user"), HttpGet]
        public HttpResponseMessage GetByUser()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Course> response = new ItemsResponse<Course>();
            string userId = UserService.GetCurrentUserId();
            response.Items = _coursesService.GetByUserId(userId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new SucessResponse();
            _coursesService.Delete(id);
            return Request.CreateResponse(response);

        }

        [Route("{id:int}/tags/{tagId:int}"), HttpPut]
        public HttpResponseMessage CourseTagInsert(int id, int tagId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _coursesService.AddCourseTags(id, tagId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}/tags/{tagId:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id, int tagId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();

            _coursesService.DeleteCourseTag(id, tagId);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}/prereqs/{preqId:int}"), HttpPut]
        public HttpResponseMessage CoursePrereqInsert(int id, int preqId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _coursesService.AddCoursePrereqs(id, preqId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}/prereqs/{preqId:int}"), HttpDelete]
        public HttpResponseMessage DeletePrereq(int id, int preqId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();

            _coursesService.DeleteCoursePrereqs(id, preqId);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}/courseUpdate"), HttpPut]
        public HttpResponseMessage UpdateCourse(TrackCourseUpdate model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _coursesService.UpdateCourse(model, id);
            return Request.CreateResponse(response);
        }

    }
}
