using Sabio.Web.Models.Requests;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;
using System.Data;
using Sabio.Data;
using Sabio.Web.Services.Interfaces;
using System.Configuration;

namespace Sabio.Web.Services
{

    public class AttendanceService : BaseService, IAttendanceService
    {


        public Attendance Add(AttendanceAddRequest model, string userId)
        {
            Attendance item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_Insert"
               , inputParamMapper: delegate(SqlParameterCollection InsertAttendance)
               {
                   InsertAttendance.AddWithValue("@UserId", userId);
                   InsertAttendance.AddWithValue("@Latitude", model.Latitude);
                   InsertAttendance.AddWithValue("@Longitude", model.Longitude);

               }, map: delegate(IDataReader reader, short set)
               {
                   item = MapAttendance(reader);
               });

            return item;
        }

        public List<Attendance> getAttendanceByCampus(int id)
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectByCampusId"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@CampusId", id);

                 }, map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Attendance> getAll()
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectAllV3"
                 , inputParamMapper: null
                 , map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Attendance> getAllCheckIn()
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectByCheckInStatus "
                 , inputParamMapper: null
                 , map: delegate (IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Attendance> getAttendanceByCheckInId(int id)
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectByCheckInId"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@Id", id);

                 }, map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Attendance> getAttendanceByDay(int id, DateTime date)
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectByDateAndCampus"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@CampusId", id);
                     paramCollection.AddWithValue("@DateAdded", date);

                 }, map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Attendance> getAttendanceByUser(int Id)
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectByUserIdV2"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@Id", Id);


                 }, map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        private static Attendance MapAttendance(IDataReader reader)
        {
            Attendance item = new Attendance();
            int startingIndex = 0;


            item.Id = reader.GetSafeInt32(startingIndex++);
            item.CampusId = reader.GetSafeInt32(startingIndex++);
            item.UserId = reader.GetSafeString(startingIndex++);
            item.DateAdded = reader.GetSafeDateTime(startingIndex++);
            item.Latitude = reader.GetSafeDoubleNullable(startingIndex++);
            item.Longitude = reader.GetSafeDoubleNullable(startingIndex++);
            item.CheckedIn = reader.GetSafeBool(startingIndex++);
            item.DistanceInMeters = reader.GetSafeDoubleNullable(startingIndex++);
            item.CampusName = reader.GetSafeString(startingIndex++);

            item.FirstName = reader.GetSafeString(startingIndex++);
            item.LastName = reader.GetSafeString(startingIndex++);
            item.UserName = reader.GetSafeString(startingIndex++);
            item.Email = reader.GetSafeString(startingIndex++);
            item.AvatarPhotoPath = reader.GetSafeString(startingIndex++);
            item.AvatarPhotoUrl = "https://"
                + ConfigurationManager.AppSettings["Bucketname"]
                + "." + ConfigurationManager.AppSettings["AWSBaseUrl"]
                + "/" + ConfigurationManager.AppSettings["AWSFolderName"]
                + "/" + item.AvatarPhotoPath;

            item.EnrollmentStatusID = reader.GetSafeInt32(startingIndex++);

            return item;
        }

        public List<Attendance> GetAllNearby(int id)
        {
            List<Attendance> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Attendance_SelectNearbyStudents"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@CampusId", id);

                 }, map: delegate(IDataReader reader, short set)
                 {

                     Attendance c = MapAttendance(reader);

                     if (List == null)
                     {
                         List = new List<Attendance>();
                     }

                     List.Add(c);
                 });

            return List;
        }

        public List<Campus> GetCampuses()
        {
            List<Campus> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Campuses_SelectAll"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   Campus camps = new Campus();
                   int startingIndex = 0;

                   camps.Id = reader.GetSafeInt32(startingIndex++);
                   camps.Name = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Campus>();
                   }

                   list.Add(camps);
               }
               );
            return list;
        }
    }
}