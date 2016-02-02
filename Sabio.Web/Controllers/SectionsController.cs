
using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("sections")]
    public class SectionsController : BaseController
    {
        public SectionsController(ISiteConfig config) : base(config)
        { }

        [Route("Add")]
        [Route("{id:int}/Edit")]
        [Route("{id:int}/Delete")]
        public ActionResult Manage(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View("Backup", model);
        }

    }
 
    
}