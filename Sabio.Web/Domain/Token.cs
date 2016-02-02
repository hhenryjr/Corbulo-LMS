using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Token
    {
        public object TokenId { get; internal set; }

        public string UserName { get; internal set; }

        public object TokenType { get; internal set; }

        public DateTime DateAdded { get; internal set; }

        public DateTime DateModified { get; internal set; }
    }
}