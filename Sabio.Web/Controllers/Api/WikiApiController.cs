using Glimpse.AspNet.Tab;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Services.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/wiki")]
    public class WikiApiController : ApiController
    {
        private IWikiContentService _wikiContentService;
        private IWikiSpaceService _wikiSpaceService;
        private IWikiService _wikiService;

        public WikiApiController(IWikiContentService wikiContentService, IWikiSpaceService wikiSpaceService, IWikiService wikiService)
        {
            _wikiContentService = wikiContentService;
            _wikiSpaceService = wikiSpaceService;
            _wikiService = wikiService;
        }

        [Route("slug/{slug}"), HttpGet]
        public HttpResponseMessage GetPageBySlug(string slug)
        {
            ItemResponse<WikiPage> response = new ItemResponse<WikiPage>();

            response.Item = _wikiContentService.GetPageBySlug(slug);

            if(null == response.Item)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "slug not found");

            return Request.CreateResponse(response);
        }

        [Route("type/{typeId:int}"), HttpGet]
        public HttpResponseMessage GetWikis([FromUri]WikiPagesFilterRequest model, int typeId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<WikiPage> wikiPagesByType = _wikiService.GetWikisByType(typeId);

            PagedList<WikiPage> FilteredWikiPagesByType = new PagedList<WikiPage>(wikiPagesByType, model.PageIndex, model.PageSize);

            ItemResponse<PagedList<WikiPage>> response = new ItemResponse<PagedList<WikiPage>>();

            response.Item = FilteredWikiPagesByType;

            return Request.CreateResponse(response);
        }

        [Route, HttpPost]
        public HttpResponseMessage Add(WikiAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            string userId = UserService.GetCurrentUserId();
            response.Item = _wikiService.Add(model, userId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(WikiUpdateRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string userId = UserService.GetCurrentUserId();

            SucessResponse response = new SucessResponse();
            _wikiService.Update(model, userId);
            return Request.CreateResponse(response);

        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage getWiki(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<WikiPage> response = new ItemResponse<WikiPage>();
            response.Item = _wikiService.GetWiki(id);
            return Request.CreateResponse(response);
        }

        [Route("Spaces"), HttpGet]
        public HttpResponseMessage GetSpaces()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<WikiSpace> response = new ItemsResponse<WikiSpace>();

            response.Items = _wikiSpaceService.Get();

            return Request.CreateResponse(response);
        }

        [Route("Delete/{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteWiki(int id)
        {
            // ItemResponse<Sabio.Web.Domain.TrackItem> response = new ItemResponse<Sabio.Web.Domain.TrackItem>();

            SucessResponse response = new SucessResponse();
            _wikiService.Delete(id);

            return Request.CreateResponse(response);
        }

        
        [Route("pages"), HttpGet]
        public HttpResponseMessage Pages()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<WikiPage> response = new ItemsResponse<WikiPage>();

            response.Items = _wikiService.Getpages();

            return Request.CreateResponse(response);

        }

        [Route("spaces/{spaceId:int}/pages"),HttpGet]
        public HttpResponseMessage Get(int spaceId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemsResponse<WikiPage> response = new ItemsResponse<WikiPage>();

            response.Items = _wikiService.GetSpaceById(spaceId);

            return Request.CreateResponse(response);

        }

        [Route("{pageId:int}/Parent/{newParentId:int}"), HttpPut]
        public HttpResponseMessage ParentId(int pageId, int? newParentId)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (newParentId == 0)
            {
                newParentId = null;
            }
            SucessResponse response = new SucessResponse();
            _wikiService.UpdateParent(pageId, newParentId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}/spaces/{spaceId:int}"), HttpPut]
        public HttpResponseMessage SpacePut(int id,int spaceId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string userId = UserService.GetCurrentUserId();
            SucessResponse response = new SucessResponse();
            _wikiService.UpdateSpaces(id,spaceId);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}/spaces/{spaceId:int}"), HttpDelete]
        public HttpResponseMessage DeleteSpace(int id, int spaceId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _wikiService.DeleteSpace(id, spaceId);
            return Request.CreateResponse(response);
        }

        // End Points For Tags 
        [Route("{id:int}/tags/{tagId:int}"),HttpPut]
        public HttpResponseMessage TagInsert(int id, int tagId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            SucessResponse response = new SucessResponse();
            _wikiService.updateTags(id, tagId);
            return Request.CreateResponse(response);
        }


        [Route("tags/{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();

            _wikiService.DeleteTag(id);

            return Request.CreateResponse(response);
        }


    }

}
