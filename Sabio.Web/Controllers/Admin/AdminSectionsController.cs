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
    [Authorize] //  TODO: add role check here
    [RoutePrefix("admin/sections")]
    public class AdminSectionsController : BaseController
    {
        public AdminSectionsController(ISiteConfig config) : base(config)
        { }

        [Route]
        public ActionResult Index()
        {
            SectionViewModel model = new SectionViewModel();

            model = DecorateViewModel<SectionViewModel>(model);

            model.CourseFormat = CourseFormat.Live;
            model.EnrollmentStatus = EnrollmentStatus.Closed;
            model.TimeZones = TimeZones.getDictionary();
            model.WikiPageContentType = WikiPageContentType.NotSet;

            return View(model);
        }        
    }
}