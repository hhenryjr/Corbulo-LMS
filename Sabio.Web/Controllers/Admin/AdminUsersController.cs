using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Admin
{
    [Authorize] //  TODO: add role check here
    [RoutePrefix("admin/users")]
    public class AdminUsersController : BaseController
    {
    
        [Route("{id:int}/sections")]
        public ActionResult AssignSections(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

        [Route()]
        public ActionResult UserList()
        {
            return View();
        }

    }
}