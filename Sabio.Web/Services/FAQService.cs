using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests.Tests;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Controllers.Api;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class FaqService : BaseService, IFaqService
    {
        public Int32 Insert(FAQAddRequest model)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.FAQ_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CategoryId", model.CategoryId);
                   paramCollection.AddWithValue("@LanguageId", model.LanguageId);
                   paramCollection.AddWithValue("@Question", model.Question);
                   paramCollection.AddWithValue("@Answer", model.Answer);


                   SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   Int32.TryParse(param["@id"].Value.ToString(), out id);
               }
               );


            return id;
        }

        public void Update(FAQUpdateRequest model)
        {


            DataProvider.ExecuteNonQuery(GetConnection, "dbo.FAQ_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CategoryId", model.CategoryId);
                   paramCollection.AddWithValue("@LanguageId", model.LanguageId);
                   paramCollection.AddWithValue("@Question", model.Question);
                   paramCollection.AddWithValue("@Answer", model.Answer);
                   paramCollection.AddWithValue("@Id", model.Id);

               }, returnParameters: delegate (SqlParameterCollection param)
               {

               }
               );

        }

        public FAQ Get(int FAQId)
        {
            FAQ f = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FAQs_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", FAQId);

               }, map: delegate (IDataReader reader, short set)
               {
                   f = new FAQ();
                   int startingIndex = 0; //startingOrdinal
                   f.Id = reader.GetSafeInt32(startingIndex++);
                   f.CategoryId = reader.GetSafeInt32(startingIndex++);
                   f.LanguageId = reader.GetSafeInt32(startingIndex++);
                   f.Question = reader.GetSafeString(startingIndex++);
                   f.Answer = reader.GetSafeString(startingIndex++);
               }
               );

            return f;
        }

        public List<FAQ> GetFAQList()
        {
            List<FAQ> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FAQs_SelectAll"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {

            },
                map: delegate (IDataReader reader, short set)
               {
                   FAQ faq = new FAQ();
                   int startingIndex = 0; //startingOrdinal
                   faq.Id = reader.GetSafeInt32(startingIndex++);
                   faq.CategoryId = reader.GetSafeInt32(startingIndex++);
                   faq.LanguageId = reader.GetSafeInt32(startingIndex++);
                   faq.Question = reader.GetSafeString(startingIndex++);
                   faq.Answer = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<FAQ>();
                   }

                   list.Add(faq);
               }
               );
            return list;
        }

        public void Delete(int FAQId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.FAQs_DeleteById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", FAQId);

            }
             );

        }

        public List<FAQ> list { get; set; }

        public object FAQId { get; set; }

        public FAQ select { get; set; }


    }


}