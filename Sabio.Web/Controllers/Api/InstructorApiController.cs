using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/Instructors")]
    public class InstructorApiController : ApiController
    {
        private IInstructorServices _instructorsService;
        //private IUserService _userService;

        public InstructorApiController(IInstructorServices instructorsService/*, IUserService UserService*/)
        {
            _instructorsService = instructorsService;
            //_userService = UserService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Add(AddInstructorInfo model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            string userId = UserService.GetCurrentUserId();
            response.Item = _instructorsService.InsertInstructor(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(UpdateInstructorInfo model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _instructorsService.Update(model);
            return Request.CreateResponse(response);

        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetInstructorsById(int id)
        {

            ItemResponse<Instructors> response = new ItemResponse<Instructors>();

            response.Item = _instructorsService.GetInstructors(id);

            return Request.CreateResponse(response);

        }

        [Route("List"), HttpGet]
        public HttpResponseMessage GetInstructors()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Instructors> response = new ItemsResponse<Instructors>();

            response.Items = _instructorsService.Instructors();

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteInstructors(int id)
        {
            // ItemResponse<Sabio.Web.Domain.TrackItem> response = new ItemResponse<Sabio.Web.Domain.TrackItem>();

            SucessResponse response = new SucessResponse();
            _instructorsService.Delete(id);

            return Request.CreateResponse(response);
        }


    }







}




