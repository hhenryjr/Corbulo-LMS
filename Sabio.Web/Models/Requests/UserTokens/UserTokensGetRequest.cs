using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.UserTokens
{
    public class UserTokensGetRequest<T>
    {
        [Required]
        public List<T> Items { get; set; }
    }
}