using Sabio.Web.Models.Requests.Payment;
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
    [RoutePrefix("api/admin/payment")]
    public class AdminPaymentApiController : ApiController
    {
        private IPaymentService _paymentService;

        public AdminPaymentApiController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [Route, HttpPost]
        public HttpResponseMessage Add(PaymentRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            string userId = UserService.GetCurrentUserId();
            response.Item = _paymentService.Add(model,userId);
            return Request.CreateResponse(response);
        }
    }
}
