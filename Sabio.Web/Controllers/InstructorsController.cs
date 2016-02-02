using Sabio.Web.Classes;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("Instructors")]
    public class InstructorsController : BaseController
    {
        public InstructorsController(ISiteConfig config) : base(config)
        { }

        // GET: Instructors
        [Route("Bio")]
        [Route("{id:int}/edit")]
        public ActionResult Bio(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

        [Route("Display")]
        public ActionResult Display()
        {
            return View();
        }


        [Route("Biodisplay")]
        public ActionResult Biodisplay()
        {
            return View();
        
               
        }




     }
 }
    
