using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests.UserTokens
{
    public class UserTokensAddRequest
    {
        public Guid TokenId { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }     
        
        public int TokenType { get; set; }
    }
}