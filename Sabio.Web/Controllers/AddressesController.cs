using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("Addresses")]
    public class AddressesController : BaseController
    {

        public AddressesController(ISiteConfig config) : base(config)
        { }

        // GET ADDRESSES
        [Route]
        public ActionResult Addresses()
        {
            return View();
        }

        [Route("Add")]  //  ADD ADDRESS
        [Route("{id:int}/edit")] //  GET ADDRESS
        public ActionResult Index(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

        [Route("Test")]
        public ActionResult Test()
        {
            return View();
        }

    }
}