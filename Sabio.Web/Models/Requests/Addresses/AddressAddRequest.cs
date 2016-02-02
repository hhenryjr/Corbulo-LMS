using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests.Addresses
{
    public class AddressAddRequest
    {    

        [Required]
        public string Line1 { get; set; }

        
        public string Line2 { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string City { get; set; }
        
        [Required]
        public int StateProvinceId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }
}