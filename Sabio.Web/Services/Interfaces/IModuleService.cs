using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IModuleService
    {
        int InsertModule(ModuleAddRequest model);

        int UpdateModules(ModuleUpdateRequest model);

        Module GetClassModules(int id);

        List<Module> GetModulesList();

        List<Module> GetSectionModules(int sectionId);

        List<ModuleWikiPages> GetModuleWikis(int moduleId);

        void DeleteModule(int moduleId);

        void AddWikiPage(int moduleId, int wikiPId);

        void DeleteWikiPage(string moduleId, string wikiPId);   //  TODO: change these to int
    }
}
