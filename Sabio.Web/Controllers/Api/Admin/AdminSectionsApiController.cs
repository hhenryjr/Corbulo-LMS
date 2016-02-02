using Sabio.Web.Domain;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Sections;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Admin
{
    [RoutePrefix("api/admin/sections")]
    public class AdminSectionsApiController : BaseAdminApiController
    {
        private ISectionService _sectionService;

        public AdminSectionsApiController(ISectionService sectionService)
        {
            this._sectionService = sectionService;
        }

        [Route, HttpGet]
        public HttpResponseMessage Index()
        {
            ItemsResponse<Section> response = new ItemsResponse<Section>();

            response.Items = _sectionService.GetAll();

            return Request.CreateResponse(response);
        }

        [Route, HttpPost]
        public HttpResponseMessage Create(SectionsAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            string userId = UserService.GetCurrentUserId();
            response.Item = _sectionService.Create(userId, model);
            return Request.CreateResponse(response);
        }

        //UPDATE SECTION
        [Route("{sectionId:int}"), HttpPut]
        public HttpResponseMessage Update(int sectionId, SectionsUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            _sectionService.Update(sectionId, model);

            return Request.CreateResponse(response);
        }

        //DELETE SECTION
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new SucessResponse();
            _sectionService.Delete(id);
            return Request.CreateResponse(response);
        }

        [Route("Info/{id:int}"), HttpPut]
        public HttpResponseMessage updateInfo(int id, SectionDescriptionUpdate model)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _sectionService.UpdateInfo(id, model);  
            return Request.CreateResponse(response);
        }

        [Route("Location/{id:int}"), HttpPut]
        public HttpResponseMessage updateLocation(int id, SectionLocation model)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _sectionService.PutLocation(id, model);
            return Request.CreateResponse(response);
        }

        [Route("{instrutorId:int}/Instructor/{id:int}"), HttpPost]
        public HttpResponseMessage addSectionInstructors(int instrutorId ,int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _sectionService.addSectionInstructors(instrutorId, id);
            return Request.CreateResponse(response);
        }

        [Route("{instrutorId:int}/Instructor/{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteSectionInstructor(int instrutorId, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _sectionService.DeleteInstructor(instrutorId, id);
            return Request.CreateResponse(response);
        }

         [Route("Instructor/{id:int}"), HttpGet]
         public HttpResponseMessage getSection(int id)
         {

             ItemsResponse<SectionInstructors> response = new ItemsResponse<SectionInstructors>();

           response.Items =  _sectionService.GetChosenInstructors(id);

             return Request.CreateResponse(response);
         }

        [Route("user/{id:int}"), HttpGet]
        public HttpResponseMessage Gets(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<SectionEnrollment> response = new ItemsResponse<SectionEnrollment>();
            response.Items =_sectionService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("{userId}/User/{sectionId:int}"), HttpPut]
        public HttpResponseMessage ParentId(string userId, int sectionId)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _sectionService.InsertSectionStudent(userId, sectionId);
            return Request.CreateResponse(response);
        }

        [Route("{userId}/User/{sectionId:int}"), HttpDelete]
        public HttpResponseMessage Student(string userId,int sectionId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _sectionService.DeleteSectionSudent(userId, sectionId);
            return Request.CreateResponse(response);
        }


        // -------------------------------------------------------------------- //
        // -------------from old sections api --------------------------------- //


        //GET SECTION BY ID
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetSection(int id)
        {
            ItemResponse<Section> response = new ItemResponse<Section>();

            response.Item = _sectionService.GetSection(id);

            return Request.CreateResponse(response);
        }

        // GET SECTIONS (LIST)
        [Route("list"), HttpGet]
        public HttpResponseMessage GetSections()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Section> response = new ItemsResponse<Section>();

            response.Items = _sectionService.GetSections();

            return Request.CreateResponse(response);
        }


        // GET SECTIONS (LIST)
        [Route("list/{courseId:int}"), HttpGet]
        public HttpResponseMessage GetSectionsByCourseId(int courseId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Section> response = new ItemsResponse<Section>();

            response.Items = _sectionService.GetSectionsByCourseId(courseId);

            return Request.CreateResponse(response);
        }

        // GET SECTIONS (LIST)
        [Route("list/user"), HttpGet]
        public HttpResponseMessage GetSectionsByUserId()
        {
            HttpResponseMessage responseMessage = null;
            BaseResponse reply= null;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string UserId = UserService.GetCurrentUserId();
            if (UserId == null) 
            {
                reply = new ErrorResponse("Please register with Sabio.");
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, reply);
            }

            ItemsResponse<UserSection> response = new ItemsResponse<UserSection>();
            response.Items = _sectionService.GetSectionsByUserId(UserId);
            return Request.CreateResponse(response);
        }

        //GET CAMPUS LIST
        [Route("campuses"), HttpGet]
        public HttpResponseMessage GetCampuses()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Campus> response = new ItemsResponse<Campus>();

            response.Items = _sectionService.GetCampuses();

            return Request.CreateResponse(response);
        }

        [Route("register/user"), HttpPost]
        public HttpResponseMessage AddUsers(UserSectionAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            string userId = UserService.GetCurrentUserId();
            response.Item = _sectionService.AddUserSection(model, userId);

            return Request.CreateResponse(response);
        }

    }
}
