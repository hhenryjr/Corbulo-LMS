using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sabio.Web.Controllers.Admin
{

    [Authorize]
    [System.Web.Mvc.RoutePrefix("admin/attendance")]
    public class AdminAttendanceController : BaseController
    {
        public AdminAttendanceController(ISiteConfig config)
            : base(config)
        { }

        [System.Web.Mvc.Route]
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            model = DecorateViewModel<BaseViewModel>(model);
            return View(model);
        }

        [Route("nearby")]
        public ActionResult NearbyStudents()
        {
            BaseViewModel model = new BaseViewModel();
            model = DecorateViewModel<BaseViewModel>(model);
            return View(model);
        }



    }

}


