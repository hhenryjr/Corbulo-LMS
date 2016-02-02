using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;
using System.Data.SqlClient;
using System.Data;
using Sabio.Data;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Models.Requests.Modules;
using Sabio.Web.Services;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Domain.Wiki;



namespace Sabio.Web.Services
{
    public class SectionModuleService : BaseService, ISectionModuleService
    {
        private IModuleService _moduleService;
        private IWikiContentService _wikiContentService;
        private IWikiService _wikiService;

        public SectionModuleService(IModuleService moduleService, IWikiContentService wikiContentService, IWikiService wikiService)
        {
            _moduleService = moduleService;
            _wikiContentService = wikiContentService;
            _wikiService = wikiService;
        }

        public int CreateSectionModule(SectionModule model)
        {
            int Id = 0;
            int moduleId = model.ModuleId;
            //Module oldModule = new Module();
            if (model.ModuleId > 0)
            {


                Module oldModule = _moduleService.GetClassModules(moduleId);


                string name = oldModule.ModuleName;
                int sectionId = model.SectionId;

                ModuleAddRequest module = new ModuleAddRequest();
                module.ModuleName = name;
                module.SectionId = sectionId;

                int newId = _moduleService.InsertModule(module);
                List<ModuleWikiPages> oldWikiPages = _moduleService.GetModuleWikis(moduleId);

                Id = newId;
                if (oldWikiPages != null)
                {
                    foreach (ModuleWikiPages wikiPage in oldWikiPages)
                    {
                        int wikiPageId = wikiPage.WikiPageId;
                        string userId = UserService.GetCurrentUserId();

                        WikiPage copyWikiPage = _wikiService.GetWiki(wikiPageId);
                        WikiAddRequest newWikiPage = new WikiAddRequest();
                        newWikiPage.Title = copyWikiPage.Title;
                        newWikiPage.URL = copyWikiPage.URL;
                        //newWikiPage.Language = copyWikiPage.Language;
                        //newWikiPage.PublishDate = copyWikiPage.PublishDate;
                        


                        int newPageId = _wikiService.Add(newWikiPage, userId);

                        _moduleService.AddWikiPage(Id, newPageId);
                    }
                }

               

            }
            return Id;
        }
    }
}