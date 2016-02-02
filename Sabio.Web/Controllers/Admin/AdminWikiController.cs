using Sabio.Web.Classes;
using Sabio.Web.Enums;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Admin
{
    [Authorize] //  TODO: add role check here
    [RoutePrefix("admin/wiki")] 
    public class AdminWikiController : BaseController
    {
        public AdminWikiController(ISiteConfig config) : base(config)        
        {
           
        }

        [Route]
        public ActionResult Index()
        {
            WikiViewModel model = new WikiViewModel();

            DecorateViewModel<WikiViewModel>(model);
            
            model.ContentTypesEnum = WikiPageContentType.NotSet;
            model.HighlightLanguagesEnum = HighlightLanguages.getDictionary();

            return View(model);
        }

        [Route("Add")]
        [Route("{id:int}/edit")]
        public ActionResult Manage(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
        [Route("tree")]
        public ActionResult Tree()
        {
            return View();
        }
    }
}