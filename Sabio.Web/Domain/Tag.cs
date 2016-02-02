using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Tag
    {
        public int Id { get; set; }
       
        public string TagName { get; set; }

        public bool Approved { get; set; }

    }
}