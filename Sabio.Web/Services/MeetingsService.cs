using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Meeting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{

    public class MeetingsService : BaseService, IMeetingService
    {

        public int Insert(MeetingAddRequest model)
        {
            int id = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Meetings_Insert";
                    SetCommonParameters(model, cmd.Parameters);
                    cmd.Parameters.AddOutputParameter("@Id", SqlDbType.Int);
                    cmd.ExecuteNonQuery();
                    int.TryParse(cmd.Parameters["@Id"].Value.ToString(), out id);
                }
            }

            return id;
        }

        public int InsertOld(MeetingAddRequest model)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Meetings_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   SetCommonParameters(model, paramCollection);

                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, 
               returnParameters: delegate(SqlParameterCollection param)
                   {
                       int.TryParse(param["@Id"].Value.ToString(), out id);
                   }
               );

            return id;
        }

        private static void SetCommonParameters(MeetingAddRequest model, SqlParameterCollection parms)
        {
            parms.AddWithValue("@Name", model.Name);
            parms.AddWithValue("@Description", model.Description);
            parms.AddWithValue("@Date", model.Date);
            parms.AddWithValue("@StartTime", model.StartTime);
            parms.AddWithValue("@EndTime", model.EndTime);
            parms.AddWithValue("@MeetingTypeId", model.MeetingTypeId);
            parms.AddWithValue("@MeetingFormatId", model.MeetingFormatId);
            parms.AddWithValue("@IsTentative", model.Tentative);
            parms.AddWithValue("@IsPublic", model.Public);
            parms.AddWithValue("@UserId", UserService.GetCurrentUserId());
        }

        public void Update(MeetingUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Meetings_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                   {
                       paramCollection.AddWithValue("@Name", model.Name);
                       paramCollection.AddWithValue("@Description", model.Description);
                       paramCollection.AddWithValue("@Date", model.Date);
                       paramCollection.AddWithValue("@StartTime", model.StartTime);
                       paramCollection.AddWithValue("@EndTime", model.EndTime);
                       paramCollection.AddWithValue("@MeetingTypeId", model.MeetingTypeId);
                       paramCollection.AddWithValue("@MeetingFormatId", model.MeetingFormatId);
                       paramCollection.AddWithValue("@IsTentative", model.Tentative);
                       paramCollection.AddWithValue("@IsPublic", model.Public);
                       paramCollection.AddWithValue("@UserId", UserService.GetCurrentUserId());
                       paramCollection.AddWithValue("@Id", model.Id);
                   }
               );
        }

        public Meeting GetById(int id)
        {
            Meeting m = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Meetings_SelectById";
                    cmd.Parameters.AddWithValue("@Id", id);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        m = MapMeeting(dr);
                    }  
                }
            }
            return m;
        }

        public Meeting GetByIdOld(int id)
        {
            Meeting m = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Meetings_SelectById",
               inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               },
               map: delegate(IDataReader reader, short set)
               {
                   m = MapMeeting(reader);
               }
               );
            return m;
        }

        public List<Meeting> GetAll()
        {
            List<Meeting> list = new List<Meeting>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Meetings_SelectAll";
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while(reader.Read())
                    {
                        Meeting m = MapMeeting(reader);
                        list.Add(m);
                    }
                }
            }
            return list;
        }

        public List<Meeting> GetAllOld()
        {
            List<Meeting> list = new List<Meeting>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Meetings_SelectAll",
               inputParamMapper: null,
               map: delegate (IDataReader reader, short set)
               {
                   Meeting m = MapMeeting(reader);
                   list.Add(m);
               }
               );
            return list;
        }

        private static Meeting MapMeeting(IDataReader reader)
        {
            Meeting m = new Meeting();
            int startingIndex = 0; //startingOrdinal
            m.Id = reader.GetSafeInt32(startingIndex++);
            m.Name = reader.GetSafeString(startingIndex++);
            m.Description = reader.GetSafeString(startingIndex++);
            m.Date = reader.GetSafeDateTime(startingIndex++);
            m.StartTime = reader.GetSafeDateTime(startingIndex++).TimeOfDay;
            m.EndTime = reader.GetSafeDateTime(startingIndex++).TimeOfDay;
            m.MeetingTypeId = reader.GetSafeInt32(startingIndex++);
            m.MeetingFormatId = reader.GetSafeInt32(startingIndex++);
            m.Tentative = reader.GetSafeBool(startingIndex++);
            m.Public = reader.GetSafeBool(startingIndex++);
            m.DateAdded = reader.GetSafeDateTime(startingIndex++);
            m.DateModified = reader.GetSafeDateTime(startingIndex++);
            return m;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Meetings_Delete"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               }
               );
        }
    }
}