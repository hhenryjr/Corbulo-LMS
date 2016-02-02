using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("Tracks")]
    [AllowAnonymous]
    public class TrackController : BaseController
    {
        public TrackController(ISiteConfig config) : base(config)
        { }

        [Route]
        public ActionResult UserIndex()
        {
            return View();
        }


        // GET:Track
        //[Route("Add")]
        //[Route("{id:int}/Edit")]
        //[Route("{id:int}/Delete")]
        //public ActionResult Index(int id = 0)
        //{
        //    ItemViewModel<int> model = new ItemViewModel<int>();
        //    model.Item = id;
        //    return View(model);
        //}

        //[Route]
        //public ActionResult ListTrackBeta()
        //{
        //    var myList = CourseFormat.Live.ToDictionaryByValue();

        //    TrackViewModel model = new TrackViewModel();
        //    model.Formats = myList;

        //    return View(model);
        //}

    }
}