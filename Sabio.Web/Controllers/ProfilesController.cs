using Sabio.Web.Classes;
using Sabio.Web.Enums;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("user")]
    public class ProfilesController : BaseController
    {
        public ProfilesController(ISiteConfig config) : base(config)
        { }

        // GET: Profiles
        [Route("current")]
        public ActionResult UserProfile()
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
