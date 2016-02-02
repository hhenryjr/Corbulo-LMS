using Microsoft.Practices.Unity;
//using Sabio.Web.Controllers.Attributes;
using Sabio.Web.Domain;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Tags;
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
    [RoutePrefix("api/tags")]
    public class TagsApiController : ApiController
    {
        //private IUserService _userService;
        private ITagsService _tagsService;

        public TagsApiController(/*IUserService UserService, */ITagsService TagsService)
        {
            //_userService = UserService;
            _tagsService = TagsService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Add(TagsAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            string userId = UserService.GetCurrentUserId();
            response.Item = _tagsService.Add(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Edit(TagsUpdateRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _tagsService.Update(model);
            return Request.CreateResponse(response);

        }

        [Route("{id:int}/active"), HttpPut]
        public HttpResponseMessage EditActive(TagsUpdateActiveRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            _tagsService.UpdateActive(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            ItemResponse<Tag> response = new ItemResponse<Tag>();

            response.Item = _tagsService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Tag> response = new ItemsResponse<Tag>();
            response.Items = _tagsService.Get();
            return Request.CreateResponse(response);


        }


    }
}
