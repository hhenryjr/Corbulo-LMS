using Sabio.Web.Classes;
using Sabio.Web.Models.Responses;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [AllowAnonymous]
    public class PublicController : BaseController
    {
        public PublicController(ISiteConfig config) : base(config)
        { }

        // GET: Public
        [Route("~/Login")]
        [Route("~/")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("~/Logout")]
        public ActionResult Logout()
        {
            UserService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Route("~/Register")]
        public ActionResult Register()
        {
            return View();
        }
        
        [Route("~/Confirm/{token:guid}")]
        public ActionResult ConfirmEmail(Guid token)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = token;
            return View(model);
        }

        [Route("~/ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("~/ResetPassword/{token:guid}")]
        public ActionResult ResetPassword(Guid token)
        {
            return View(token);
        }        
    }
}

