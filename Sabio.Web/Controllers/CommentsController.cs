using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("Comments")]
    public class CommentsController : BaseController
    {
        public CommentsController(ISiteConfig config) : base(config)
        { }
        // GET: Comments
        [Route("View")]
        public ActionResult Index()
        {
            CommentViewModel model = new CommentViewModel();
            DecorateViewModel<CommentViewModel>(model);
            model.OwnerType = CommentOwnerType.NotSet;
                
            return View(model);
        }
    }
}