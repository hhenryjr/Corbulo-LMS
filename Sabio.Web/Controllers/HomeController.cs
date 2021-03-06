﻿using Sabio.Web.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// ********** FYI: System Generated File

namespace Sabio.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ISiteConfig config) : base(config)
        { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}