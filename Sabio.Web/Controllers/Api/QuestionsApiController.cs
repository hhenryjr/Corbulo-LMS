using Sabio.Web.Domain;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("api/questions")]
    public class QuestionsAPIController : ApiController
    {
        private IOfficeHourServices _officeHourServices;

        public QuestionsAPIController(IOfficeHourServices OfficeHourServices)
        {
            _officeHourServices = OfficeHourServices;
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetQuestions(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<OfficeHourQuestion> response = new ItemsResponse<OfficeHourQuestion>();

            response.Items = _officeHourServices.GetByOfficeHourId(id);

            return Request.CreateResponse(response);
        }

        [Route("Add/{Id:int}"),HttpPost]
        public HttpResponseMessage Post(int Id,OfficeHourQuestion model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
             SucessResponse response = new SucessResponse();
            string userId = UserService.GetCurrentUserId();
            _officeHourServices.InsertQuestion(Id,model,userId);
            return Request.CreateResponse(response);

        }
    }
}
