using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using Sabio.Data;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Services
{
    public class EventsService : BaseService

    {
        public static int InsertEvents(EventsAddRequest model)
        {
            int eventId = 0; //

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Events_Insert"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@EventName", model.EventName);
               paramCollection.AddWithValue("@EventVenue", model.Venue);
               paramCollection.AddWithValue("@Date", model.Date);
               paramCollection.AddWithValue("@Phone", model.PhoneNumber);


               SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
               p.Direction = System.Data.ParameterDirection.Output;

               paramCollection.Add(p);

           }, returnParameters: delegate (SqlParameterCollection param)
           {
               int.TryParse(param["@Id"].Value.ToString(), out eventId);
           }
           );


            return eventId;
        }

        public static void Update(Models.Request.EventsUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Events_Insert"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@Id", model.Id);
               paramCollection.AddWithValue("@EventName", model.EventName);
               paramCollection.AddWithValue("@EventVenue", model.Venue);
               paramCollection.AddWithValue("@Date", model.Date);
               paramCollection.AddWithValue("@Phone", model.PhoneNumber);

           });




        }



    }

}
