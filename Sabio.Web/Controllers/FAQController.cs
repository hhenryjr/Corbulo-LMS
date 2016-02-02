using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sabio.Web.Controllers
{
    [RoutePrefix("FAQ")]
    public class FaqController : BaseController
    {
        public FaqController(ISiteConfig config) : base(config)
        {        }

        [Route("add")]
        [Route("{id:int}")]
        public ActionResult IndexForm(int id=0)
        {
           var model = GetViewModel<ItemViewModel<int>>();
            model.Item = id;
            
            return View(model);
        }
        [Route]
        public ActionResult List()
        {
            var model = GetViewModel<BaseViewModel>();
            return View(model);
        }
    }
}