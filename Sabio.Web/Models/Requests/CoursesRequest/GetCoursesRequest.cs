using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.CoursesRequest
{
    public class GetCoursesRequest<C>
    {
        public List<C> Items { get; set; }
    }
}