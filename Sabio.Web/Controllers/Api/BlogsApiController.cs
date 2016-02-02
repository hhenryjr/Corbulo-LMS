using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Blogs;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api.Blogs
{
    [RoutePrefix("api/blogs")]
    public class BlogsApiController : ApiController
    {
        private IBlogService _blogService;

        public BlogsApiController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route(), HttpPost]
        public HttpResponseMessage AddBlog(BlogAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            //string userId = UserService.GetCurrentUserId();
            response.Item = _blogService.InsertBlog(model);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateBlog(BlogUpdateRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            _blogService.UpdateBlog(model);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {       
            ItemResponse<Blog> response = new ItemResponse<Blog>();

            response.Item = _blogService.Get(id);

            return Request.CreateResponse(response);
        }

        [Route(), HttpGet]
        public HttpResponseMessage BlogList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Blog> response = new ItemsResponse<Blog>();

            response.Items = _blogService.GetBlogList();

            return Request.CreateResponse(response);
        }

        [Route("blogTags"), HttpGet]
        public HttpResponseMessage getBlogTags()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<BlogTag> response = new ItemsResponse<BlogTag>();
            response.Items = _blogService.getTagsUsedByBlogs();
            return Request.CreateResponse(response);
        }

        [Route("tags/{tagName}"), HttpGet]
        public HttpResponseMessage BlogListByTagName(string tagName)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Blog> response = new ItemsResponse<Blog>();

            response.Items = _blogService.GetBlogsByTagName(tagName);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteBlog(int id)
        {
            SucessResponse response = new SucessResponse();
            _blogService.Delete(id);

            return Request.CreateResponse(response);
        }
    }    
}
