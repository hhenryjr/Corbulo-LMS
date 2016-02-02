using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Data;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Classes;

namespace Sabio.Web.Controllers
{   [RoutePrefix("Instructor")]
    public class InstructorProfileController : BaseController
    {
        public InstructorProfileController(ISiteConfig config) : base(config)
        { }

        // GET: InstructorProfile
        [Route("{id:int}")]
        public ActionResult Index(int id=0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
    }
}