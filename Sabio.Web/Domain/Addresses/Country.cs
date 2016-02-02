﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Addresses
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string LongCode { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
    }
}
