using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Addresses
{
    public class Address
    {
        public int Id { get; set; }       
        
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public StateProvince StateProvince { get; set; }

        //public StateProvince CountryName { get; set; }

        //public string StateProvinceCode { get; set; }

        //public string CountryName { get; set; }

        //public int CountryId { get; set; }
    }
}