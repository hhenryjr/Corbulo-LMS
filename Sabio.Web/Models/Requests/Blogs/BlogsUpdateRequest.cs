﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Blogs
{
    public class BlogUpdateRequest : BlogAddRequest
    {
       [Required] 
        public int Id { get; set; }
    }
}