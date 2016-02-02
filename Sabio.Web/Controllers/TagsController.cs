using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Data;
using Sabio.Web.Models.ViewModels;

namespace Sabio.Web.Controllers
{   [RoutePrefix("tags")]
    public class TagsController : Controller
    {
        // GET: Tags
        public ActionResult Index(int id=0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

       
    }
}