using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web
{
    public enum WikiPageContentType 
    {
        NotSet = 0,
        Text = 1,
        Code = 2,
        List = 3,
        Image = 4,
        Highlight = 5,
        Wiki = 6
    }
}