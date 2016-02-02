using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Payment
{
    public class Card
    {
        public string Id { get; set; }
        public int Last4 { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public int Exp_Month { get; set; }
        public int Exp_Year { get; set; }
        public string Funding { get; set; }
        public string Object { get; set; }
        public string Name { get; set; }

    }
}