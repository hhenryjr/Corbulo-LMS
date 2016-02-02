
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api

    // API Controller is Required for An endpoint to Ajax Call 
    // Endpoint is a method in Api Controller 
{
    [AllowAnonymous]
    [RoutePrefix("api/message")]
    public class MessageApiController : BaseApiController 
    {
        public IMessagingService _messagingService;

        public MessageApiController(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        [Route("ContactUs"), HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendMail(MailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            await _messagingService.Mail(model);

            return Request.CreateResponse(response);

        }

        [Route("ConfirmEmail"), HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendConfirmMail(SendConfirmationEmailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            await _messagingService.SendConfirmationEmail(model);

            return Request.CreateResponse(response);

        }

        [Route("ForgotPass"), HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SendForgotPasswordEmail(SendConfirmationEmailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            SucessResponse response = new SucessResponse();

            await _messagingService.SendConfirmationEmail(model);

            return Request.CreateResponse(response);

        }

    }
}
