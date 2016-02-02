using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Sections;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class SectionService : BaseService, ISectionService
    {
        private ICoursesService _coursesService;

        public SectionService(ICoursesService CoursesService)
        {
            _coursesService = CoursesService;
        }


        public int Create(string userId, SectionsAddRequest model)
        {
            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Sections_Insert_v3",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {

                    parameterCollection.AddWithValue("@CourseId", model.CourseId);
                    parameterCollection.AddWithValue("@StartDate", model.StartDate);
                    parameterCollection.AddWithValue("@EndDate", model.EndDate);
                    parameterCollection.AddWithValue("@DaysOfWeek", model.DaysOfWeek);
                    parameterCollection.AddWithValue("@StartTime", model.StartTime);
                    parameterCollection.AddWithValue("@EndTime", model.EndTime);
                    parameterCollection.AddWithValue("@TimeZone", model.TimeZone);
                    //parameterCollection.AddWithValue("@Info", model.Info);
                    parameterCollection.AddWithValue("@Format", model.Format);
                    //parameterCollection.AddWithValue("@CampusLocation", model.CampusLocation);
                    //parameterCollection.AddWithValue("@RoomNumber", model.RoomNumber);
                    parameterCollection.AddWithValue("@Capacity", model.Capacity);
                    //parameterCollection.AddWithValue("@CurrentEnrollment", model.CurrentEnrollment);
                    parameterCollection.AddWithValue("@Status", model.Status);
                    parameterCollection.AddWithValue("@RegistrationDeadline", model.RegistrationDeadline);
                    parameterCollection.AddWithValue("@UserId", userId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterCollection.Add(p);


                }, returnParameters: delegate(SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                );

            //foreach (var Id in model.Instructors)
            //    DataProvider.ExecuteNonQuery(GetConnection, "SectionInstructors_InsertV2",
            //        inputParamMapper: delegate (SqlParameterCollection param)
            //        {
            //            param.AddWithValue("@SectionId", id);
            //            param.AddWithValue("@InstructorId", Id);
            //        });


            return id;
        }

        public void Update(int sectionId, SectionsUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Sections_Update_v2",
                inputParamMapper: delegate(SqlParameterCollection updateSection)
                {
                    updateSection.AddWithValue("@Id", model.Id);
                    updateSection.AddWithValue("@CourseId", model.CourseId);
                    updateSection.AddWithValue("@DaysOfWeek", model.DaysOfWeek);
                    updateSection.AddWithValue("@StartDate", model.StartDate);
                    updateSection.AddWithValue("@EndDate", model.EndDate);
                    updateSection.AddWithValue("@RegistrationDeadline", model.RegistrationDeadline);
                    updateSection.AddWithValue("@StartTime", model.StartTime);
                    updateSection.AddWithValue("@EndTime", model.EndTime);
                    updateSection.AddWithValue("@TimeZone", model.TimeZone);
                    //updateSection.AddWithValue("@Info", model.Info);
                    updateSection.AddWithValue("@Format", model.Format);
                    //updateSection.AddWithValue("@CampusLocation", model.CampusLocation);
                    //updateSection.AddWithValue("@RoomNumber", model.RoomNumber);
                    updateSection.AddWithValue("@Capacity", model.Capacity);
                    //updateSection.AddWithValue("@CurrentEnrollment", model.CurrentEnrollment);
                    updateSection.AddWithValue("@Status", model.Status);
                });
            //DataProvider.ExecuteNonQuery(GetConnection, "dbo.SectionInstructors_DeleteBySectionId",
            //    inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            //    {
            //        parameterCollection.AddWithValue("@SectionId", model.Id);
            //    });
            //foreach (var InstructorId in model.Instructors)
            //    DataProvider.ExecuteNonQuery(GetConnection, "dbo.SectionInstructors_InsertV2",
            //        inputParamMapper: delegate (SqlParameterCollection param)
            //        {
            //            param.AddWithValue("@SectionId", model.Id);
            //            param.AddWithValue("@InstructorId", InstructorId);
            //        });
        }

        public List<Section> GetAll()
        {
            List<Section> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Sections_AdminSelectAll"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   Section section = MapSection(reader);
                   if (list == null)
                   {
                       list = new List<Section>();
                   }

                   list.Add(section);
               }
               );
            return list;
        }

        public void UpdateInfo(int id, SectionDescriptionUpdate model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Sections_UpdateDescription"
                     , inputParamMapper: delegate(SqlParameterCollection param)
                     {
                         param.AddWithValue("@Id", id);
                         param.AddWithValue("@Info", model.Info);
                     });
        }

        public void PutLocation(int id, SectionLocation model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Sections_UpdateLocationTab"
                     , inputParamMapper: delegate(SqlParameterCollection param)
                     {
                         param.AddWithValue("@Id", id);
                         param.AddWithValue("@CampusLocation", model.CampusLocation);
                         param.AddWithValue("@RoomNumber", model.RoomNumber);
                     });
        }

        public void addSectionInstructors(int instrutorId, int Id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.SectionInstructors_InsertV2",
                inputParamMapper: delegate(SqlParameterCollection param)
                {
                    param.AddWithValue("@SectionId", Id);
                    param.AddWithValue("@InstructorId", instrutorId);
                });
        }

        public void DeleteInstructor(int instrutorId, int Id)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.SectionInstructors_DeleteBySectionId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@SectionId", Id);
                    parameterCollection.AddWithValue("@InstructorId", instrutorId);
                });
        }

        public List<SectionInstructors> GetChosenInstructors(int id)
        {
            List<SectionInstructors> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.SectionInstructors_SelectBySectionId"
                 , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                     {
                         paramCollection.AddWithValue("@Id", id);

                     }, map: delegate(IDataReader reader, short set)
        {

            SectionInstructors si = SectionInstructorsMapCourse(reader);

            if (List == null)
            {
                List = new List<SectionInstructors>();
            }

            List.Add(si);
        });

            return List;
        }

        public List<SectionEnrollment> Get(int id)
        {
            List<SectionEnrollment> List = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.UserSections_selectBySectionId"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);

               }, map: delegate(IDataReader reader, short set)
               {
                   SectionEnrollment s = new SectionEnrollment();

                   int startingIndex = 0;

                   s.SectionId = reader.GetSafeInt32(startingIndex++);
                   // s.EnrollmentStatusId = reader.GetSafeInt32(startingIndex++);
                   s.FirstName = reader.GetSafeString(startingIndex++);
                   s.LastName = reader.GetSafeString(startingIndex++);
                   s.Email = reader.GetSafeString(startingIndex++);
                   s.UserId = reader.GetSafeString(startingIndex++);

                   if (List == null)
                   {
                       List = new List<SectionEnrollment>();
                   }
                   List.Add(s);

               });
            return List;
        }

        public void InsertSectionStudent(string id, int sectionId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserSections_InsertV2DragDrop"
                     , inputParamMapper: delegate(SqlParameterCollection param)
                     {
                         param.AddWithValue("@UserId", id);
                         param.AddWithValue("@SectionId", sectionId);
                     });
        }

        public void DeleteSectionSudent(string userId, int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserSections_DeleteBySectionId"
               , inputParamMapper: delegate(SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@SectionId", id);
                   parameterCollection.AddWithValue("@UserId", userId);
               });
        }

        private Section MapSection(IDataReader reader)
        {
            Section sec = new Section();
            int startingIndex = 0;

            sec.Id = reader.GetSafeInt32(startingIndex++);
            sec.CourseId = reader.GetSafeInt32(startingIndex++);
            sec.DaysOfWeek = reader.GetSafeInt32(startingIndex++);
            sec.StartDate = reader.GetSafeDateTime(startingIndex++);
            sec.EndDate = reader.GetSafeDateTime(startingIndex++);
            sec.RegistrationDeadline = reader.GetSafeDateTime(startingIndex++);
            sec.StartTime = reader.GetSafeInt32(startingIndex++);
            sec.EndTime = reader.GetSafeInt32(startingIndex++);
            sec.TimeZone = reader.GetSafeInt32(startingIndex++);
            sec.Info = reader.GetSafeString(startingIndex++);
            sec.Format = reader.GetSafeInt32(startingIndex++);
            sec.CampusLocation = reader.GetSafeInt32(startingIndex++);
            sec.RoomNumber = reader.GetSafeString(startingIndex++);
            sec.Capacity = reader.GetSafeInt32(startingIndex++);
            sec.CurrentEnrollment = reader.GetSafeInt32(startingIndex++);
            sec.Status = reader.GetSafeInt32(startingIndex++);
            sec.Name = reader.GetSafeString(startingIndex++);
            CourseBase cb = new CourseBase();
            cb.Id = reader.GetSafeInt32(startingIndex++);
            cb.CourseName = reader.GetSafeString(startingIndex++);
            sec.Course = cb;
            Campus campus = new Campus();
            campus.Id = reader.GetSafeInt32(startingIndex++);
            campus.Name = reader.GetSafeString(startingIndex++);
            sec.Campus = campus;


            return sec;

        }


        private SectionInstructors SectionInstructorsMapCourse(IDataReader reader)
        {
            SectionInstructors item = new SectionInstructors();
            int startingIndex = 0;

            item.SectionId = reader.GetSafeInt32(startingIndex++);
            item.InstructorId = reader.GetSafeInt32(startingIndex++);
            item.Name = reader.GetSafeString(startingIndex++);
            return item;
        }

        public Section GetSection(int id)
        {
            Section sections = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Sections_SelectByIdV4"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);

               }, map: delegate(IDataReader reader, short set)
               {
                   if (set == 0)
                   {
                       sections = MapSection(reader);
                   }
                   else if (set == 1)
                   {
                       SectionInstructors si = SectionInstructorsMapCourse(reader);
                       if (sections.Instructors == null)
                       {
                           sections.Instructors = new List<SectionInstructors>();
                       }
                       sections.Instructors.Add(si);
                   }
               }
               );
            return sections;
        }

        public List<Section> GetSections()
        {
            List<Section> sec = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Sections_SelectAllV2"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   Section item = MapSection(reader);

                   if (sec == null)
                   {
                       sec = new List<Section>();
                   }

                   sec.Add(item);
               });
            return sec;
        }


        public List<Section> GetSectionsByCourseId(int CourseId)
        {
            List<Section> sec = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Sections_SelectAllByCourseIdAndUserId"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CourseId", CourseId);
                   paramCollection.AddWithValue("@UserId", UserService.GetCurrentUserId());

               }, map: delegate(IDataReader reader, short set)
               {
                   Section item = new Section();
                   //Part of the old sectionservice
                   //Course c = new Course();
                   Campus cm = new Campus();
                   int startingIndex = 0;

                   item.Id = reader.GetSafeInt32(startingIndex++);
                   item.CourseId = reader.GetSafeInt32(startingIndex++);
                   item.DaysOfWeek = reader.GetSafeInt32(startingIndex++);
                   item.StartDate = reader.GetSafeDateTime(startingIndex++);
                   item.EndDate = reader.GetSafeDateTime(startingIndex++);
                   item.RegistrationDeadline = reader.GetSafeDateTime(startingIndex++);
                   item.StartTime = reader.GetSafeInt32(startingIndex++);
                   item.EndTime = reader.GetSafeInt32(startingIndex++);
                   item.TimeZone = reader.GetSafeInt32(startingIndex++);
                   item.Info = reader.GetSafeString(startingIndex++);
                   item.Format = reader.GetSafeInt32(startingIndex++);
                   item.CampusLocation = reader.GetSafeInt32(startingIndex++);
                   item.RoomNumber = reader.GetSafeString(startingIndex++);
                   item.Capacity = reader.GetSafeInt32(startingIndex++);
                   item.CurrentEnrollment = reader.GetSafeInt32(startingIndex++);
                   item.Status = reader.GetSafeInt32(startingIndex++);
                   cm.Name = reader.GetSafeString(startingIndex++);
                   item.EnrollmentStatusId = reader.GetSafeInt32(startingIndex++);
 
                
                   item.Campus = cm;
                   //part of the old section service that returns campus id, campus name, course id and course name
                   //cm.Id = reader.GetSafeInt32(startingIndex++);
                   //cm.Name = reader.GetSafeString(startingIndex++);
                   //c.Id = reader.GetSafeInt32(startingIndex++);
                   //c.CourseName = reader.GetSafeString(startingIndex++);

                   //item.Course = c;
                  
                   //return item;

                   if (sec == null)
                   {
                       sec = new List<Section>();
                   }

                   sec.Add(item);
               });
            return sec;
        }

        public List<UserSection> GetSectionsByUserId(string UserId)
        {
            List<UserSection> usersec = null;

            DataProvider.ExecuteCmd(GetConnection, "UserSections_selectByUserId"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", UserId);

               }, map: delegate(IDataReader reader, short set)
               {
                   UserSection item = new UserSection();
                   int startingIndex = 0;

                   item.SectionId = reader.GetSafeInt32(startingIndex++);
                   item.EnrollmentStatusId = reader.GetSafeInt32(startingIndex++);

                   if (usersec == null)
                   {
                       usersec = new List<UserSection>();
                   }

                   usersec.Add(item);
               });
            return usersec;
        }


        public List<Campus> GetCampuses()
        {
            List<Campus> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Campuses_SelectAll"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
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

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Sections_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.SectionInstructors_DeleteBySectionId",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@SectionId", id);
                });

        }

        public int AddUserSection(UserSectionAddRequest model, string userId)
        {
            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserSections_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", userId);
                    paramCollection.AddWithValue("@SectionId", model.SectionId);
                    paramCollection.AddWithValue("@EnrollmentStatusId", model.EnrollmentStatusId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCollection.Add(p);
                },
                returnParameters: delegate(SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                );
            return id;

        }



        public Section GetSectionDetailsByUserId(string UserId)
        {
            Section us = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.UserSections_GetSectionDetailsByUserId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@UserId", UserId);
                },
                map: delegate(IDataReader reader, short set)
                {
                    us = MapSection(reader);
                }

                );
            return us;

        }




    }

}
