using Sabio.Web.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Admin
{
    public class AdminPaymentController : BaseController
    {
        public AdminPaymentController(ISiteConfig config) : base(config)
        {
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}