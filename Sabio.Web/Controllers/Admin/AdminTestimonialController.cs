using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Classes;

namespace Sabio.Web.Controllers.Admin
{
    [RoutePrefix("testimonial")]
    public class AdminTestimonialController : BaseController
    {
        public AdminTestimonialController(ISiteConfig config) : base(config) { }

        [Route("create")]//post
        [Route("{id:int}")]//get
        public ActionResult CreateTestimonial(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

        [Route("list")]
        public ActionResult TestimonialList()
        {
            //ItemViewModel<int> model = new ItemViewModel<int>();
            return View();
        }
    }
}