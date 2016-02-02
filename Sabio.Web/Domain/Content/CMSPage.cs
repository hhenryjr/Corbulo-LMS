using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class CMSPage
    {
        public int Id {get; set;}
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public DateTime DateToPublish { get; set; }
        public DateTime DateToExpire { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Message { get; set; }
    }
}       