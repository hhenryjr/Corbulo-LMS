using Sabio.Web.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("tests")]
    public class TestsController : BaseController
    {
        public TestsController(ISiteConfig config) : base(config)
        { }

        // GET: Tests
        [Route("ajax")]
        public ActionResult Ajax()
        {
            return View();
        }

        // GET: Tests
        [Route("angular")]
        public ActionResult Angular()
        {
            return View();
        }

        [Route("angular/bootstrap")]
        public ActionResult AngularBootstrap()
        {
            return View();
        }

        [Route("angular/routes")]
        public ActionResult AngularRoutes()
        {
            return View();
        }

        [Route("angular/events")]
        public ActionResult AngularEvents()
        {
            return View();
        }

        [Route("angular/basic")]
        public ActionResult AngularBasic()
        {
            return View();
        }

        // GET: Tests
        [Route("fileUpload")]
        public ActionResult FileUpload()
        {
            return View();
        }

        [Route("Wizards")]
        public ActionResult Wizards()
        {
            return View();
        }

    }
}