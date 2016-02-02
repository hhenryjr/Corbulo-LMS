using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Payment
{
    public class PaymentRequest
    {
        public int PaymentId { get; set; }
        public string Id { get; set; }
        public int Created { get; set; }
        public string LiveMode { get; set; }
        public string Object { get; set; }
        public string Used { get; set; }

        public List<Card> Cards { get; set; }
    }

}