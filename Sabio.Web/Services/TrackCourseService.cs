using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests.Track;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class TrackCourseService : BaseService , ITrackCourseService
    {
        public TrackCourse Get(int id)
        {
            TrackCourse s = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.TrackCourses_SelectById",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@TracksId", id);
                },
                map: delegate (IDataReader reader, short set)
                {
                    s = new TrackCourse();
                    int startingIndex = 0;

                   
                    s.CourseId = reader.GetSafeInt32(startingIndex++);
                    s.Order = reader.GetSafeInt32(startingIndex++);
                }

                );
            return s;

        }

        public int Insert(TrackCourseRequest model)
        {

            var trackId = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Insert",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    
                    parameterCollection.AddWithValue("@TracksId", model.CourseId);
                    parameterCollection.AddWithValue("@Order", model.Order);

                },

                returnParameters:delegate (SqlParameterCollection para)
                {
                    int.TryParse(para["@TracksId"].Value.ToString(), out trackId);
                });

            return trackId;
        }

        public  void Update(TrackCourseUpdate model)
        {
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Update",
               inputParamMapper: delegate (SqlParameterCollection u)
               {
                  
                   u.AddWithValue("@TracksId", model.Id);
                   u.AddWithValue("@Order", model.Order);

               });
            }

        }

        public  void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.TrackCourses_Delete",
             inputParamMapper: delegate (SqlParameterCollection parameterCollection)
             {
                 parameterCollection.AddWithValue("@TracksId", id);
             });
        }

        public  List<TrackCourse> GetByTrack(int id)
        {
            List<TrackCourse> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.TrackCourses_SelectByTrackID",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@TracksId", id);
                   
                },
                map: delegate (IDataReader reader, short set)
                {
                    TrackCourse s = new TrackCourse();
                    int startingIndex = 0;

                    s.CourseId = reader.GetSafeInt32(startingIndex++);
                    s.Order = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<TrackCourse>();
                    }
                    list.Add(s);
                }

                );
            return list;
        }
    }
}