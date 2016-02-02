using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Domain;
using Sabio.Web.Classes;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("Meetings")]
    public class MeetingsController : BaseController
    {
        IMeetingService _meetingService;

        public MeetingsController(ISiteConfig config, IMeetingService meetingService) : base(config)
        {
            _meetingService = meetingService;
        }

        // Route /Meetings/Add to add, /Meetings/5 to edit
        [Route("{id:int}")]
        [Route("Add")]
        public ActionResult Add(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int> { Item = id };
            return View(model);
        }

        // Route /Meetings
        [Route]
        public ActionResult Index()
        {
            ItemViewModel<IEnumerable<Meeting>> model = new ItemViewModel<IEnumerable<Meeting>>();
            model.Item = _meetingService.GetAll();
            return View(model);
        }

        [Route("Angular")]
        public ActionResult Angular()
        {
            BaseViewModel model = new BaseViewModel();
            return View(model);
        }

    }
}