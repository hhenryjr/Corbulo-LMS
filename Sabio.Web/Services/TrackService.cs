using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;

//Sabio: including this namespace is critical to use some of the utility funcitons we have built into our code
// these utility functions are Extension Methods built off of IDataReader
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Track;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{                              //the ":" denotes that Trackservice inherits every thing from Base Service
    public class TrackService : BaseService, ITrackService
    {
        public int Get()
        {
            return 0;
        }

        public int Add(TrackAddRequest model)
        {

            var id = 0;

            //connecting to SQL server, which will call the database and the stored procedure
            //1 database call = get connection with stored procedure below "CourseTrack_Insert"

            //ADO is a library of code that is used to communicate wiht the database 
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tracks_Insert",
                //(SQLparam)suitcase of data that the .NET will populate and send to the server
                //inline function in .NET(delegate)
                //Delegate x = fx()} delegate points to a function.
                //type                  //Name
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Name", model.Name);
                    parameterCollection.AddWithValue("@Format", model.Format);
                    parameterCollection.AddWithValue("@ExpectedOutcome", model.ExpectedOutCome);
                    parameterCollection.AddWithValue("@Cost", model.Cost);
                    parameterCollection.AddWithValue("@Description", model.Description);

                    SqlParameter s = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    s.Direction = System.Data.ParameterDirection.Output;
                    parameterCollection.Add(s);


                },

                returnParameters: delegate(SqlParameterCollection para)
                {                   //this will come from the database server
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                //when using the insert method in REST Client the intent is to see that Id will return a value to the user.
                );

            foreach (var courseId in model.CourseIds)
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Insert",
                    delegate(SqlParameterCollection para)
                    {
                        para.AddWithValue("@TracksId", id);
                        para.AddWithValue("@CourseId", courseId);
                        
                    });
            }
            return id;
        }

        public void Update(TrackUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tracks_Update",

                inputParamMapper: delegate(SqlParameterCollection updateCTrack)
               {
                   updateCTrack.AddWithValue("@Id", model.Id);
                   updateCTrack.AddWithValue("@Name", model.Name);
                   updateCTrack.AddWithValue("@Format", model.Format);
                   updateCTrack.AddWithValue("@ExpectedOutcome", model.ExpectedOutCome);
                   updateCTrack.AddWithValue("@Cost", model.Cost);
                   updateCTrack.AddWithValue("@Description", model.Description);

               });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Delete",
            inputParamMapper: delegate(SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@TracksId", model.Id);
            });

            foreach (var courseId in model.CourseIds)
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Insert",
                    delegate(SqlParameterCollection para)
                    {
                        para.AddWithValue("@TracksId", model.Id);
                        para.AddWithValue("@CourseId", courseId);
                    });
            }
        }

        public void Delete(int id)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Delete",
              inputParamMapper: delegate(SqlParameterCollection parameterCollection)
              {
                  parameterCollection.AddWithValue("@TracksId", id);
              });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tracks_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                });

        }


        public Track Get(int id)
        {
            Track item = null;

            TrackCourse getTrackCourse = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Tracks_SelectById"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
                   //model binding
               }, map: delegate(IDataReader reader, short set)
               {
                   if (set == 0)
                   {
                       item = new Track();
                       int startingIndex = 0; //startingOrdinal

                       item.Id = reader.GetSafeInt32(startingIndex++);
                       item.Name = reader.GetSafeString(startingIndex++);
                       item.Format = reader.GetSafeInt32(startingIndex++);
                       item.ExpectedOutcome = reader.GetSafeString(startingIndex++);
                       item.Cost = reader.GetSafeInt32(startingIndex++);
                       item.Description = reader.GetSafeString(startingIndex++);
                   }
                   else if (set == 1 && item != null)
                   {
                       getTrackCourse = new TrackCourse();
                       int startingIndex = 0;

                       getTrackCourse.CourseId = reader.GetSafeInt32(startingIndex++);
                       getTrackCourse.Order = reader.GetSafeInt32(startingIndex++);
                       getTrackCourse.CourseName = reader.GetSafeString(startingIndex);

                       if (item.TracksCourses == null)
                       {
                           item.TracksCourses = new List<TrackCourse>();
                       }

                       if( item.CourseIds == null)
                       {
                           item.CourseIds = new List<int>();
                       }

                       item.TracksCourses.Add(getTrackCourse);
                       item.CourseIds.Add(getTrackCourse.CourseId);

                   }

                   else if (set == 2 && item != null)
                   {
                       CourseTrack prereq = new CourseTrack();
                       int startingIndex = 0;

                       prereq.CourseName = reader.GetSafeString(startingIndex++);
                       prereq.Id = reader.GetSafeInt32(startingIndex);

                       if (item.Prerequisites == null)
                       {
                           item.Prerequisites = new List<CourseTrack>();
                       }


                       item.Prerequisites.Add(prereq);

                   }

               });

            return item;

        }

        public List<Track> GetTrackList()
        {
            List<Track> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Tracks_SelectAll"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   if (set == 0)
                   {
                       Track ct = new Track();
                       int startingIndex = 0;

                       ct.Id = reader.GetSafeInt32(startingIndex++);
                       ct.Name = reader.GetSafeString(startingIndex++);
                       ct.Format = reader.GetSafeInt32(startingIndex++);
                       ct.ExpectedOutcome = reader.GetSafeString(startingIndex++);
                       ct.Cost = reader.GetSafeInt32(startingIndex++);
                       ct.Description = reader.GetSafeString(startingIndex++);

                       if (list == null)
                       {
                           list = new List<Track>();
                       }

                       list.Add(ct);
                   }
                   else if (set == 1 && list != null)
                   {
                       TrackCourse item = new TrackCourse();
                       int startingIndex = 0;

                       var TracksId = reader.GetSafeInt32(startingIndex++);
                       item.CourseId = reader.GetSafeInt32(startingIndex++);
                       item.Order = reader.GetSafeInt32(startingIndex++);
                       item.CourseName = reader.GetSafeString(startingIndex++);
                       foreach (var track in list)
                       {
                           if (track.Id == TracksId)
                           {
                               if (track.TracksCourses == null)
                               {
                                   track.TracksCourses = new List<TrackCourse>();
                               }

                               track.TracksCourses.Add(item);
                           }
                       }
                   }

                   else if (set == 2 && list != null)
                   {
                       CourseTrack prereq = new CourseTrack();
                       int startingIndex = 0;

                       prereq.TracksId = reader.GetSafeInt32(startingIndex++);
                       prereq.CourseName = reader.GetSafeString(startingIndex++);
                       prereq.Id = reader.GetSafeInt32(startingIndex);


                       foreach (var track in list)
                       {
                           if (track.Id == prereq.TracksId)
                           {
                               if (track.Prerequisites == null)
                               {
                                   track.Prerequisites = new List<CourseTrack>();
                               }
                               track.Prerequisites.Add(prereq);
                           }
                       }
                   }
               });

            return list;
        }

        public CourseSection GetSection(int id)
        {
            CourseSection item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Sections_SelectByCourseId",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            },
            map: delegate(IDataReader reader, short set)
         {
             item = new CourseSection();
             int startingIndex = 0;

             item.Id = reader.GetSafeInt32(startingIndex++);
             item.CourseId = reader.GetSafeInt32(startingIndex++);
             item.Info = reader.GetSafeString(startingIndex++);

         }
        );
            return item;
        }

    }
}