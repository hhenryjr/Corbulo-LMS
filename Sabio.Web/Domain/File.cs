using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class File : FileBase
    {
        public int WikiContentId { get; set; }

        public string UserId { get; set; }

        public bool InUse { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public string Url { get; set; }
    }
}