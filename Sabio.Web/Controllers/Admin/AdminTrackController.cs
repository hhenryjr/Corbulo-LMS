using Sabio.Web.Classes;
using Sabio.Web.Enums;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Admin
{
    [Authorize] //TODO
    [RoutePrefix("admin/track")]
    public class AdminTrackController : BaseController
    {
        public AdminTrackController(ISiteConfig config) : base(config)
        {  }

        // GET: AdminTrack

        [Route]
        public ActionResult Index()
        {
            SectionViewModel model = new SectionViewModel();
            model = DecorateViewModel<SectionViewModel>(model);
            model.CourseFormat = CourseFormat.Live;
            return View(model);
        }        



    }
}