using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models;
using Sabio.Web.Classes;

namespace Sabio.Web.Controllers
{
    public class EventsController : BaseController
    {
        public EventsController(ISiteConfig config) : base(config)
        { }

        // GET: Events
        public ActionResult Index()
        {
            //ViewData["CurrentTime"] = DateTime.Now.ToString();
            return View();
        }
        

    }
}