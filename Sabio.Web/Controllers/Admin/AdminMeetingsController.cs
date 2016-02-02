using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Admin
{
    [Authorize] //  TODO: add role check here
    [RoutePrefix("admin/meetings")]
    public class AdminMeetingsController : BaseController
    {
        [Route]
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            model = DecorateViewModel<BaseViewModel>(model);

            return View(model);
        }
    }
}