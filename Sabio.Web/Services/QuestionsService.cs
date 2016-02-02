using Sabio.Data;
using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Sabio.Web.Services
{
    public class QuestionsService : BaseService
    {
        public static List<OfficeHourQuestion> GetByOfficeHourId(int id)
        {
            List<OfficeHourQuestion> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.OfficeHourQuestions_SelectByOfficeHourId"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@OfficeHourId", id);

               }, map: delegate (IDataReader reader, short set)
               {
                   OfficeHourQuestion question = OfficeHourQuestionMap(reader);

                   if (list == null)
                   {
                       list = new List<OfficeHourQuestion>();
                   }
                   list.Add(question);
               });
            return list;
        }

        private static OfficeHourQuestion OfficeHourQuestionMap(IDataReader reader)
        {
            OfficeHourQuestion item = new OfficeHourQuestion();
            int startingIndex = 0;

            item.Id = reader.GetSafeInt32(startingIndex++);
            item.OfficeHourId = reader.GetSafeInt32(startingIndex++);
            item.Question = reader.GetSafeString(startingIndex++);
            item.Response = reader.GetSafeString(startingIndex++);
            item.Grouping = reader.GetSafeString(startingIndex++);
            item.QuestionStatusId = reader.GetSafeInt32(startingIndex++);

            return item;
        }
    }
}