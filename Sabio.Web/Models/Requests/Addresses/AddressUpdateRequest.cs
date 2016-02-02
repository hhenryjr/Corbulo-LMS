using Sabio.Web.Models.Requests.Addresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Sabio.Web.Models.Requests.Addresses
{
    public class AddressUpdateRequest : AddressAddRequest //inheriting from AddressAddRequest
    {
        [Required]
        public int Id { get; set; }  //applying this as an addition to AddressAddRequest 
    }
}
