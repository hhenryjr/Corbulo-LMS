using Sabio.Web.Models.Requests.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        int Add(PaymentRequest model, string userId);
    }
}