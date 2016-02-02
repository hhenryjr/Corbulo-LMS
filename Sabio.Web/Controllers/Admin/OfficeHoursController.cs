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
    [RoutePrefix("Admin/OfficeHours")]
    public class OfficeHoursController : BaseController
    {
        public OfficeHoursController(ISiteConfig config)
            : base(config)
        { }


        [Route("Add")]
        [Route("{id:int}")]
        public ActionResult OfficeHours(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

        [Route]
        public ActionResult Index()
        {
            SectionViewModel model = new SectionViewModel();

            model = DecorateViewModel<SectionViewModel>(model);     
            model.TimeZones = TimeZones.getDictionary();         

            return View(model);
        }
    }
}