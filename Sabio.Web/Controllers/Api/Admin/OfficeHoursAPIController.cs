using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.OfficeHours;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/OfficeHour")]
    public class OfficeHoursAPIController : ApiController
    {

        private IOfficeHourServices _officeHourServices;
        private IMessagingService _messagingService;

        public OfficeHoursAPIController(IOfficeHourServices OfficeHourServices, IMessagingService messagingService)
        {
            this._officeHourServices = OfficeHourServices;
            this._messagingService = messagingService;
        }


        [AllowAnonymous]
        [Route("Add"), HttpPost]
        //public HttpResponseMessage Add(OfficeHourAddRequest model)
         public async Task<HttpResponseMessage> Add(OfficeHourAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            string userId = UserService.GetCurrentUserId();
            response.Item = _officeHourServices.Add(model, userId);

            await SendEmail(model);
            //ItemsResponse<UserSection> EmailList = new ItemsResponse<UserSection>();
            //EmailList.Items = _officeHourServices.GetEmailList(model.SectionId);

            //foreach (var items in EmailList.Items)
            //{
            //    await _messagingService.SendAddOfficeHourEmail(items.Email, model);         
            //}

            return Request.CreateResponse(response);
        }

        private async Task<HttpResponseMessage> SendEmail(OfficeHourAddRequest model)
        {
            ItemsResponse<UserSection> EmailList = new ItemsResponse<UserSection>();
            EmailList.Items = _officeHourServices.GetEmailList(model.SectionId);

            SucessResponse sent = new SucessResponse();
            foreach (var items in EmailList.Items)
            {
                await _messagingService.SendAddOfficeHourEmail(items.Email, model);         
            }
            return Request.CreateResponse(sent);
        }

        [Route("{id:int}/Edit"), HttpPut]
        public async Task<HttpResponseMessage> Edit(OfficeHourUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();
            string userId = UserService.GetCurrentUserId();
            _officeHourServices.Update(model, userId);

            await SendEmail(model);
            //ItemsResponse<UserSection> EmailList = new ItemsResponse<UserSection>();
            //EmailList.Items = _officeHourServices.GetEmailList(model.SectionId);

            //foreach (var items in EmailList.Items)
            //{
            //    await _messagingService.SendAddOfficeHourEmail(items.Email, model);
            //}

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<OfficeHour> response = new ItemResponse<OfficeHour>();
            response.Item = _officeHourServices.Get(id);
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            ItemsResponse<OfficeHour> response = new ItemsResponse<OfficeHour>();
            response.Items = _officeHourServices.GetList();
            return Request.CreateResponse(response);
        }

        [Route("{id:int}/Delete"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SucessResponse response = new SucessResponse();

            _officeHourServices.Delete(id);

            return Request.CreateResponse(response);
        }

        [Route("{oId:int}/question/{Id:int}"), HttpGet]
        public HttpResponseMessage Edit(int oId, int Id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<OfficeHourQuestion> response = new ItemResponse<OfficeHourQuestion>();
            response.Item = _officeHourServices.getQuestion(oId, Id);
            return Request.CreateResponse(response);
        }

        [Route("{oId:int}/question/{Id:int}"), HttpPut]
        public HttpResponseMessage AddUpdateResponse(OfficeHourQuestionsUpdateRequest model, int oId, int Id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _officeHourServices.AddQuestionResponse(model, oId, Id);
            return Request.CreateResponse(response);
        }

        [Route("{Id:int}/deleteQuestion"), HttpDelete]
        public HttpResponseMessage DeleteQuestion(int Id)
        {
            SucessResponse response = new SucessResponse();

            _officeHourServices.DeleteQuestion(Id);
        
            return Request.CreateResponse(response);
        }
    }
}
