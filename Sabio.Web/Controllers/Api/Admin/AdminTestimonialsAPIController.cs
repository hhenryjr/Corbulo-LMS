using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Testimonials;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api.Admin
{
    [AllowAnonymous]
    [RoutePrefix("api/admin/testimonial")]
    public class AdminTestimonialsAPIController : ApiController
    {
        private ITestimonialsService _TestimonialsService;

        public AdminTestimonialsAPIController(ITestimonialsService TestimonialsService)
        {
            this._TestimonialsService = TestimonialsService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(TestimonialsAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _TestimonialsService.Insert(model);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            ItemResponse<Testimonials> response = new ItemResponse<Testimonials>();
            response.Item = _TestimonialsService.Get(id);
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(TestimonialsUpdateRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _TestimonialsService.Update(model);
            return Request.CreateResponse(response);
        }

        [Route("List"), HttpGet]
        public HttpResponseMessage GetList()
                {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemsResponse<Testimonials> response = new ItemsResponse<Testimonials>();
            response.Items = _TestimonialsService.GetList();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new SucessResponse();
            _TestimonialsService.Delete(id);
            return Request.CreateResponse(response);
        }
    }
}
