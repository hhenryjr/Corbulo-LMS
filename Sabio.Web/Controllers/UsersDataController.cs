using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Classes;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("users/Profile")]
    public class UsersDataController : BaseController
    {


        public UsersDataController(ISiteConfig config) : base(config)
        { }

        // GET: UsersData
        [Route("Add")]
        [Route("{id:int}/Edit")]
        public ActionResult IndexAjax(int id=0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);

        }

        [Route("List")]
        public ActionResult ListAjax()
        {
            return View();
        }

    }

     
}