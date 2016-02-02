using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Data;
using Sabio.Web.Classes;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers
{
    [Authorize]
    [RoutePrefix("wiki")]
    public class WikiController : BaseController
    {
        private IWikiSpaceService _wikiSpaceService;

        public WikiController(IWikiSpaceService wikiSpaceService, ISiteConfig config) : base(config)
        {
            _wikiSpaceService = wikiSpaceService;
        }

    //  all admin functionality has been refactored into AdminWikiController        
        [Route("Pages")]
        public ActionResult Index()
        {
            WikiViewModel model = new WikiViewModel();

            DecorateViewModel<WikiViewModel>(model);

            model.Spaces = _wikiSpaceService.Get();
            model.ContentTypesEnum = WikiPageContentType.NotSet;

            return View("Home", model);
        }


        [Route]
        public ActionResult WikiPage()
        {
            WikiViewModel model = new WikiViewModel();

            DecorateViewModel<WikiViewModel>(model);

            model.Spaces = _wikiSpaceService.Get();
            model.ContentTypesEnum = WikiPageContentType.NotSet;

            return View(model);
        }    
    }
}