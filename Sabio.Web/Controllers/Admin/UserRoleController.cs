using Sabio.Web.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("admin/roles")]
    public class UserRoleController : BaseController
    {
        public UserRoleController(ISiteConfig config) : base(config)
        { }

        // GET: UserRole
        [Route("user")]
        public ActionResult Index()
        {
            return View();
        }
    }
}