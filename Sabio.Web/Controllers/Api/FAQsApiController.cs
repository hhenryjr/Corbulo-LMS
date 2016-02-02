using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/FAQ")]
    public class FAQsApiController : ApiController
    {
        private IFaqService _faqService;

        public FAQsApiController(IFaqService faqService)
        {
            this._faqService = faqService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Add(FAQAddRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _faqService.Insert(model);

            return Request.CreateResponse(response);
        }

        [Route("update"), HttpPut]
        public HttpResponseMessage Update(FAQUpdateRequest model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

            SucessResponse response = new SucessResponse();
            _faqService.Update(model);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }

            ItemResponse<FAQ> response = new ItemResponse<FAQ>();
            response.Item = _faqService.Get(id);


            return Request.CreateResponse(response);
        }

        [Route("List"), HttpGet]
        public HttpResponseMessage GetFAQList()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<FAQ> response = new ItemsResponse<FAQ>();

            response.Items = _faqService.GetFAQList();

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _faqService.Delete(id);

            return Request.CreateResponse(response);
        }

    }
}
