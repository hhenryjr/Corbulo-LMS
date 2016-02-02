using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Admin
{
    [RoutePrefix("api/admin/wiki")]
    public class AdminWikiPageContentApiController : BaseAdminApiController
    {
        private IWikiContentService _wikiContentService;
        private ICoursesService _coursesService;

        public AdminWikiPageContentApiController(IWikiContentService wikiContentService,
                                                    ICoursesService coursesService
            )
        {
            this._wikiContentService = wikiContentService;
            this._coursesService = coursesService;
        }

        [Route("content/order"), HttpPut]
        public HttpResponseMessage UpdateWikiPageContentOrder([FromBody] WikiContentOrderRequest request)
        {
            _wikiContentService.UpdateContentOrders(request);

            ItemResponse<bool> response = new ItemResponse<bool>();

            response.Item = true;

            return Request.CreateResponse(response);
        }

        [Route("content/{contentId:int}"), HttpPut]
        public HttpResponseMessage UpdateWikiPageContent(int contentId, WikiContentUpdateRequest request)
        {
            _wikiContentService.UpdateContent(contentId, request);

            ItemResponse<bool> response = new ItemResponse<bool>();

            response.Item = true;

            return Request.CreateResponse(response);
        }

        [Route("content/{contentId:int}"), HttpDelete]
        public HttpResponseMessage DeleteWikiPageContent(int contentId)
        {
            _wikiContentService.DeleteContent(contentId);

            ItemResponse<bool> response = new ItemResponse<bool>();

            response.Item = true;

            return Request.CreateResponse(response);
        }

        [Route("content"), HttpPost]
        public HttpResponseMessage CreateWikiPageContent(WikiContentCreateRequest request)
        {            
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = _wikiContentService.InsertContent(request);

            return Request.CreateResponse(response);
        }
        
        [Route("page/{pageId:int}/content"), HttpGet]
        public HttpResponseMessage GetWikiPageContent(int pageId)
        {
            ItemsResponse<WikiPageContent> response = new ItemsResponse<WikiPageContent>();

            response.Items = _wikiContentService.GetContentByPageId(pageId);

            return Request.CreateResponse(response);
        }

        [Route("{courseId:int}/Instructor/{id:int}"), HttpPost]
        public HttpResponseMessage addCourseInstructors(int courseId, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _coursesService.AddCourseInstructor(courseId, id);
            return Request.CreateResponse(response);
        }

        [Route("{courseId:int}/Instructor/{id:int}"), HttpDelete]
        public HttpResponseMessage deleteCourseInstructor(int courseId, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _coursesService.DeleteCourseInstructor(courseId, id);
            return Request.CreateResponse(response);
        }

    }
}
