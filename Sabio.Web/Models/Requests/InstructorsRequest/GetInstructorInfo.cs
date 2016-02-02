using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.InstructorsRequest
{
    public class GetInstructorInfo<C>
    {
        public List<C> Items { get; set;}
    }
}