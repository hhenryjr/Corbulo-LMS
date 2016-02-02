using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Data;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Classes;

namespace Sabio.Web.Controllers
{   [RoutePrefix("Courses")]
    public class CoursesController : BaseController
    {
        public CoursesController(ISiteConfig config) : base(config)
        { }

        // GET: Courses
        [Route("Add")]
        [Route("{id:int}/Edit")]
        
        public ActionResult Index(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

    [Route()]
    public ActionResult ListCourses()
        {
            return View();
        }

        //[Route("{id:int}/Details")]
        //public ActionResult CourseDetail(int id = 0)
        //{
        //    ItemViewModel<int> model = new ItemViewModel<int>();
        //    model.Item = id;
        //    return View(model);
        //}

        [Route("{id:int}/Details")]
        public ActionResult CourseDetails(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }



    }


}