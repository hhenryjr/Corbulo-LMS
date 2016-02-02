using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("blogs")]
    public class BlogController : BaseController
    {
        public BlogController(ISiteConfig config) : base(config)
        {
        }

        // GET: Blog
        [Route("Add")]
        [Route("{id:int}")]
        public ActionResult Index(int id = 0)
        {
            ItemViewModel<int> model = GetViewModel<ItemViewModel<int>>();
            model.Item = id;
            return View(model);
        }

        public ActionResult Blogs()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return View(model);
        }

        [Route("tags/{tagName}")]
        [Route]
        public ActionResult Tags(string tagName = null)
        {
            ItemViewModel<string> model = GetViewModel<ItemViewModel<string>>();
            model.Item = tagName;
            return View("Blogs", model);
        }
    }

}