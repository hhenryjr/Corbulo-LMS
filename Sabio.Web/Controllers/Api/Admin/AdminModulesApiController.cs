using Sabio.Web.Domain;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sabio.Web.Models.Requests.Modules;

namespace Sabio.Web.Controllers.Api.Admin
{
     [RoutePrefix("api/admin/modules")]
    public class AdminModulesApiController : BaseAdminApiController
    {
        private IModuleService _moduleService;
        private ISectionModuleService _sectionModuleService;
        private IWikiService _wikiService;

        public AdminModulesApiController(IModuleService moduleService, ISectionModuleService sectionModuleService, IWikiService wikiService)
        {
            _moduleService = moduleService;
            _sectionModuleService = sectionModuleService;
            _wikiService = wikiService;
        }

       


        [Route("section/{sectionId:int}"), HttpGet]
        public HttpResponseMessage GetSectionModules(int sectionId)
        {
            ItemsResponse<Module> response = new ItemsResponse<Module>();

            response.Items = _moduleService.GetSectionModules(sectionId);

            return Request.CreateResponse(response);
        }

        [Route("{moduleId:int}/tree"), HttpGet]
        public HttpResponseMessage GetModuleWikiPageTree(int moduleId)
        {
            ItemsResponse<WikiPage> response = new ItemsResponse<WikiPage>();

            response.Items = _wikiService.GetPagesByModule(moduleId);

            return Request.CreateResponse(response);
        }

        [Route("{moduleId:int}/page"), HttpPost]
        public HttpResponseMessage AddPageToModule(int moduleId, ModulePageAddRequest model)
        {
            if (ModelState.IsValid)
            {
                _moduleService.AddWikiPage(moduleId, model.WikiPageId);

                SucessResponse response = new SucessResponse();
                
                return Request.CreateResponse(response);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

         [Route("sectionmodules/{sectionId:int}"), HttpPost]
         public HttpResponseMessage AddSectionModule(SectionModule model)
        {
            if (ModelState.IsValid && model.ModuleId > 0)
            {
                _sectionModuleService.CreateSectionModule(model);

                SucessResponse response = new SucessResponse();
                return Request.CreateResponse(response);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route, HttpPost]
        public HttpResponseMessage PostClassModule(ModuleAddRequest model)
        {
            if (ModelState.IsValid)
            {
                int id = _moduleService.InsertModule(model);
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = id;
                return Request.CreateResponse(response);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //[Route("{id}/wiki/{wikiPId}"), HttpPut]
        // public HttpResponseMessage addWikiPage(string id, string wikiPId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    SucessResponse response = new SucessResponse();
        //    _moduleService.AddWikiPage(id, wikiPId);
        //    return Request.CreateResponse(response);
        //}

        [Route("{id}/wiki/{wikiPId}"), HttpDelete]
        public HttpResponseMessage deleteWikiPage(string id, string wikiPId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            SucessResponse response = new SucessResponse();
            _moduleService.DeleteWikiPage(id, wikiPId);
            return Request.CreateResponse(response);
        }



        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateClassModule(ModuleUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                int id = _moduleService.UpdateModules(model);
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetClassModules(int id)
        {
            ItemResponse<Sabio.Web.Domain.Module> response = new ItemResponse<Module>();

            response.Item = _moduleService.GetClassModules(id);

            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage List()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Module> response = new ItemsResponse<Module>();

            response.Items = _moduleService.GetModulesList();

            return Request.CreateResponse(response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteModules(int id)
        {
            SucessResponse response = new SucessResponse();
            _moduleService.DeleteModule(id);
            return Request.CreateResponse(response);

        }

        [Route("wiki/{id:int}"), HttpGet]
        public HttpResponseMessage GetWikiPageById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<ModuleWikiPages> response = new ItemsResponse<ModuleWikiPages>();

            response.Items = _moduleService.GetModuleWikis(id);

            return Request.CreateResponse(response);
        }
    }
}
