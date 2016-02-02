using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Sabio.Web.Domain;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests.CoursesRequest;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class CoursesService : BaseService, ICoursesService
    {
        public int Add(AddRequest model, string userId)
        {
            var id = 0;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Courses_Insert_V4",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@CourseName", model.CourseName);
                    parameterCollection.AddWithValue("@Length", model.Length);
                    parameterCollection.AddWithValue("@Description", model.Description);
                    parameterCollection.AddWithValue("@CourseStart", model.Start);
                    parameterCollection.AddWithValue("@CourseEnd", model.End);
                    parameterCollection.AddWithValue("@UserId", userId);
                    parameterCollection.AddWithValue("@LearningObjectives", model.LearningObjectives);
                    parameterCollection.AddWithValue("@ExpectedOutcome", model.ExpectedOutcome);
                    parameterCollection.AddWithValue("@EvaluationCriteria", model.EvaluationCriteria);
                    parameterCollection.AddWithValue("@Format", model.Format);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterCollection.Add(p);
                }, 
                returnParameters: delegate(SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                );

            foreach (var PrereqId in model.Prereqs)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", id);
                        param.AddWithValue("@PrereqId", PrereqId);
                    });

            foreach (var TagId in model.Tags)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", id);
                        param.AddWithValue("@TagId", TagId);

                    });

            foreach (var Id in model.Instructors)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", id);
                        param.AddWithValue("@InstructorId", Id);
                    });

            return id;
        }

        public void Update(CourseUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Courses_Update2",
                inputParamMapper: delegate(SqlParameterCollection updateCourse)
                {
                    updateCourse.AddWithValue("@Id", model.Id);
                    updateCourse.AddWithValue("@CourseName", model.CourseName);
                    updateCourse.AddWithValue("@Length", model.Length);
                    updateCourse.AddWithValue("@Description", model.Description);
                    updateCourse.AddWithValue("@CourseStart", model.Start);
                    updateCourse.AddWithValue("@CourseEnd", model.End);
                    updateCourse.AddWithValue("@LearningObjectives", model.LearningObjectives);
                    updateCourse.AddWithValue("@ExpectedOutcome", model.ExpectedOutcome);
                    updateCourse.AddWithValue("@EvaluationCriteria", model.EvaluationCriteria);
                    updateCourse.AddWithValue("@Format", model.Format);

                });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@CourseId", model.Id);
                });
            foreach (var PrereqId in model.Prereqs)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", model.Id);
                        param.AddWithValue("@PrereqId", PrereqId);
                    });


            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@CourseId", model.Id);
                });
            foreach (var TagId in model.Tags)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", model.Id);
                        param.AddWithValue("@TagId", TagId);
                });


            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@CourseId", model.Id);
                });
            foreach (var instructorId in model.Instructors)
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_Insert",
                    inputParamMapper: delegate(SqlParameterCollection param)
                    {
                        param.AddWithValue("@CourseId", model.Id);
                        param.AddWithValue("@InstructorId", instructorId);
                    });
        }

        public Course Get(int id)
        {
            Course course = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Courses_SelectByIdV2",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }, map: delegate(IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        course = MapCourse(reader);
                    }
                    else if (set == 1)
                    {
                        CourseTag ct = TagMapCourse(reader);
                        if (course.Tags == null)
                        {
                            course.Tags = new List<CourseTag>();
                        }
                        course.Tags.Add(ct);
                    }

                    else if (set == 2)
                    {
                        CourseInstructors imc = InstructorsMapCourse(reader);
                        if (course.Instructors == null)
                        {
                            course.Instructors = new List<CourseInstructors>();
                        }
                        course.Instructors.Add(imc);
                        }

                    else if (set == 3)
                    {
                        CourseBase bc = PrereqsMapCourse(reader);
                        if (course.Prereqs == null)
                        {
                            course.Prereqs = new List<CourseBase>();
                }
                        course.Prereqs.Add(bc);
                }
                });
            return course;

        }

        private static Course MapCourse(IDataReader reader)
        {
            Course item = new Course();
            int startingIndex = 0;

            item.Id = reader.GetSafeInt32(startingIndex++);
            item.CourseName = reader.GetSafeString(startingIndex++);
            item.Length = reader.GetSafeString(startingIndex++);
            item.Description = reader.GetSafeString(startingIndex++);
            item.Start = reader.GetSafeDateTime(startingIndex++);
            item.End = reader.GetSafeDateTime(startingIndex++);
            item.CourseLength = reader.GetSafeInt32(startingIndex++);
            item.LearningObjectives = reader.GetSafeString(startingIndex++);
            item.ExpectedOutcome = reader.GetSafeString(startingIndex++);
            item.EvaluationCriteria = reader.GetSafeString(startingIndex++);
            item.Format = reader.GetSafeInt32(startingIndex++);
            return item;
        }

        private static CourseBase PrereqsMapCourse(IDataReader reader)
        {
            CourseBase item = new CourseBase();
            int startingIndex = 0;
            item.Id = reader.GetSafeInt32(startingIndex++);
            item.CourseName = reader.GetSafeString(startingIndex++);

            return item;
        }

        private static CourseTag TagMapCourse(IDataReader reader)
        {
            CourseTag item = new CourseTag();
            int startingIndex = 0;

            item.CourseId = reader.GetSafeInt32(startingIndex++);
            item.TagId = reader.GetSafeInt32(startingIndex++);
            item.TagName = reader.GetSafeString(startingIndex++);

            return item;
        }

        private static CourseInstructors InstructorsMapCourse(IDataReader reader)
        {
            CourseInstructors item = new CourseInstructors();
            int startingIndex = 0;

            item.CourseId = reader.GetSafeInt32(startingIndex++);
            item.InstructorId = reader.GetSafeInt32(startingIndex++);
            item.Name = reader.GetSafeString(startingIndex++);

            return item;
        }

        public List<Course> Get()
        {
            List<Course> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Courses_SelectAll_V2"
                , inputParamMapper: null, map: delegate(IDataReader reader, short set)
                {
                    Course item = MapCourse(reader);

                    if (list == null)
                    {
                        list = new List<Course>();
                    }

                    list.Add(item);
                });

            return list;
        }

        public void Delete(int id)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@CourseId", id);
                });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CourseId", id);
                });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_DeleteByCourseId",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CourseId", id);
                });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Courses_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }

        public List<Course> GetByUserId(string userId)
        {
            List<Course> list = new List<Course>();
            Dictionary<int, Course> book = new Dictionary<int, Course>(); 

            DataProvider.ExecuteCmd(GetConnection, "dbo.UserCourses_SelectByUserId"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@userId", userId);
                }, map: delegate(IDataReader reader, short set)
                {
                        if (set == 0)
                        {
                            Course course = new Course();
                    int startingIndex = 0;

                            course.Id = reader.GetSafeInt32(startingIndex++);
                            course.CourseName = reader.GetSafeString(startingIndex++);
                            //course.Length = reader.GetSafeString(startingIndex++);
                            course.Description = reader.GetSafeString(startingIndex++);
                            course.Start = reader.GetSafeDateTime(startingIndex++);
                            course.End = reader.GetSafeDateTime(startingIndex++);
                            //course.Prereqs = reader.GetSafeString(startingIndex++);

                            list.Add(course);
                            book.Add(course.Id, course);
                        }
                        else if (set == 1)
                        {
                            CourseTag tag = TagMapCourse(reader);
                            Course parentCourse = book[tag.CourseId];

                            if (parentCourse.Tags == null)
                            {
                                parentCourse.Tags = new List<CourseTag>();
                            }
                            parentCourse.Tags.Add(tag);
                        }
                    });
            return list;
        }

        public void DeleteCourseInstructor(int cId, int Id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_DeleteByCourseIdAndInstructorId",
               inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CourseId", cId);
                   paramCollection.AddWithValue("@InstructorId", Id);
               });

        }

        public void AddCourseInstructor(int cId, int Id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseInstructors_Insert",
                inputParamMapper: delegate(SqlParameterCollection param)
                {
                    param.AddWithValue("@CourseId", cId);
                    param.AddWithValue("@InstructorId", Id);
                });
        }

        public void AddCourseTags(int id, int tagId)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_Insert",
                inputParamMapper: delegate(SqlParameterCollection param)
                {
                    param.AddWithValue("@CourseId", id);
                    param.AddWithValue("@TagId", tagId);
                });
        }

        public void DeleteCourseTag(int id, int tagId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CourseTags_DeleteByCourseIdandTagId"
               , inputParamMapper: delegate(SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@CourseId", id);
                   parameterCollection.AddWithValue("@TagId", tagId);
               });
        }

        public void AddCoursePrereqs(int id, int preqId)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_Insert",
                inputParamMapper: delegate(SqlParameterCollection param)
                {
                    param.AddWithValue("@CourseId", id);
                    param.AddWithValue("@PrereqId", preqId);
                });
        }

        public void DeleteCoursePrereqs(int id, int preqId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CoursePrereqs_DeleteByCourseIdAndPrereqId"
               , inputParamMapper: delegate(SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@CourseId", id);
                   parameterCollection.AddWithValue("@PrereqId", preqId);
               });
        }


        public void UpdateCourse(TrackCourseUpdate model, int Id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Courses_UpdateFromTrackEdit",
                inputParamMapper: delegate(SqlParameterCollection updateCourse)
                {
                    updateCourse.AddWithValue("@Id", Id);
                    updateCourse.AddWithValue("@CourseName", model.CourseName);
                    updateCourse.AddWithValue("@Length", model.Length);
                    updateCourse.AddWithValue("@Description", model.Description);
                    updateCourse.AddWithValue("@CourseStart", model.Start);
                    updateCourse.AddWithValue("@CourseEnd", model.End);
                    updateCourse.AddWithValue("@LearningObjectives", model.LearningObjectives);
                    updateCourse.AddWithValue("@ExpectedOutcome", model.ExpectedOutcome);
                    updateCourse.AddWithValue("@EvaluationCriteria", model.EvaluationCriteria);
                    updateCourse.AddWithValue("@Format", model.Format);

                });
        }
    }
}

