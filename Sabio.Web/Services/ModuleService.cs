using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class ModuleService : BaseService, IModuleService
    {
        public List<Module> GetSectionModules(int sectionId)
        {
            List<Module> list = null;
            DataProvider.ExecuteCmd( GetConnection, "dbo.Modules_SelectBySectionId",
            inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@SectionId", sectionId);                
            },
            map: delegate (IDataReader reader, short set)
            {
                Module module = MapModule(reader);

                if (null == list)
                    list = new List<Module>();

                list.Add(module);
            });
            return list;
        }

        public int InsertModule(ModuleAddRequest model)
        {
            int ModulesId = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Modules_Insert_v2"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ModuleName", model.ModuleName);
                   //paramCollection.AddWithValue("@Length", model.Length);
                   //paramCollection.AddWithValue("@Labs", model.Labs);
                   //paramCollection.AddWithValue("@RequiredReading", model.RequiredReading);
                   //paramCollection.AddWithValue("@Homework", model.Homework);
                   //paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@SectionId", model.SectionId);

                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);
               },
               returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out ModulesId);
               });

            return ModulesId;
        }

        public int UpdateModules(ModuleUpdateRequest model)
        {
            int ModulesId = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Modules_Update"
               , inputParamMapper: delegate(SqlParameterCollection updateModules)
               {
                   updateModules.AddWithValue("@ModuleName", model.ModuleName);
                   //updateModules.AddWithValue("@Length", model.Length);
                   //updateModules.AddWithValue("@Labs", model.Labs);
                   //updateModules.AddWithValue("@RequiredReading", model.RequiredReading);
                   //updateModules.AddWithValue("@Homework", model.Homework);
                   //updateModules.AddWithValue("@Description", model.Description);
                  // updateModules.AddWithValue("@SectionId", model.SectionId);
                   updateModules.AddWithValue("@Id", model.Id);
               },
               returnParameters: delegate(SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out ModulesId);
               });
            return ModulesId;
        }
       
        public Module GetClassModules(int id)
        {
            Module item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Modules_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    item = new Module();
                    int startingIndex = 0;

                    item.ModuleName = reader.GetSafeString(startingIndex++);
                    item.Length = reader.GetSafeInt32(startingIndex++);
                    item.Labs = reader.GetSafeString(startingIndex++);
                    item.RequiredReading = reader.GetSafeString(startingIndex++);
                    item.Homework = reader.GetSafeString(startingIndex++);
                    item.Description = reader.GetSafeString(startingIndex++);
                    item.SectionId = reader.GetSafeInt32(startingIndex++);
                    item.Id = reader.GetSafeInt32(startingIndex);
                });
            return item;
        }

        public List<Module> GetModulesList()
        {
            List<Module> list = new List<Module>();
            DataProvider.ExecuteCmd(
            GetConnection, "dbo.Modules_SelectAll",
            inputParamMapper: null,
            map: delegate (IDataReader reader, short set)
            {
                Module module = MapModule(reader);
                list.Add(module);
            });
            return list;
        }

        public List<ModuleWikiPages> GetModuleWikis(int moduleId)
        {
            List<ModuleWikiPages> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.ModuleWikiPageContent_SelectByModuleId",
            inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", moduleId);
            },
            map: delegate(IDataReader reader, short set)
            {
                ModuleWikiPages module = new ModuleWikiPages();
                int startingIndex = 0;

                module.ModuleId = reader.GetSafeInt32(startingIndex++);
                module.WikiPageId = reader.GetSafeInt32(startingIndex++);
                module.Name = reader.GetSafeString(startingIndex++);
                
                if (list == null)
                   {
                       list = new List<ModuleWikiPages>();
                   }
                 list.Add(module);
            });
            return list;
        }
       
        public void DeleteModule(int moduleId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Modules_DeleteById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", moduleId);

                });
        }

        public void AddWikiPage(int moduleId, int wikiPId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.ModuleWikiPageContent_Insert"
                     , inputParamMapper: delegate(SqlParameterCollection param)
                     {
                         param.AddWithValue("@ModuleId", moduleId);
                         param.AddWithValue("@WikiPageId", wikiPId);
                     });

        }

        public void DeleteWikiPage(string moduleId, string wikiPId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.ModuleWikiPageContent_Delete"
               , inputParamMapper: delegate(SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@ModuleId", moduleId);
                   parameterCollection.AddWithValue("@WikiPageId", wikiPId);
               });
        }

        private Module MapModule (IDataReader reader)
        {
            Module module = new Module();
            int startingIndex = 0;
            module.Id = reader.GetSafeInt32(startingIndex++);
            module.ModuleName = reader.GetSafeString(startingIndex++);
            module.Length = reader.GetSafeInt32(startingIndex++);
            module.Labs = reader.GetSafeString(startingIndex++);
            module.RequiredReading = reader.GetSafeString(startingIndex++);
            module.Homework = reader.GetSafeString(startingIndex++);
            module.Description = reader.GetSafeString(startingIndex++);
            module.SectionId = reader.GetSafeInt32(startingIndex++);
            return module;
        }
    }
}