using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain;
using System.Data;
using System.Data.SqlClient;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class InstructorsServices : BaseService, IInstructorServices
    {
       
        public int InsertInstructor(Sabio.Web.Models.Requests.AddInstructorInfo model, string userId)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Instructors_Insert"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  //paramCollection.AddWithValue("@UserId", userId);
                  paramCollection.AddWithValue("@Name", model.Name);
                  paramCollection.AddWithValue("@Email", model.Email);
                  paramCollection.AddWithValue("@Bio", model.Bio);
                  paramCollection.AddWithValue("@LinkedIn", model.LinkedIn);
                  paramCollection.AddWithValue("@CoursesTaught", String.Join("|", model.CoursesTaught));
                  

                  SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                  p.Direction = System.Data.ParameterDirection.Output;

                  paramCollection.Add(p);
              }, returnParameters: delegate (SqlParameterCollection param)
              {
                  int.TryParse(param["@Id"].Value.ToString(), out Id);

              }
              );

            return Id;
        }

        public void Update(UpdateInstructorInfo model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Instructors_Update",

               inputParamMapper: delegate (SqlParameterCollection updateinstructors)
               {
                   updateinstructors.AddWithValue("@Id", model.Id);
                   updateinstructors.AddWithValue("@Name", model.Name);
                   updateinstructors.AddWithValue("@Email", model.Email);
                   updateinstructors.AddWithValue("@Bio", model.Bio);
                   updateinstructors.AddWithValue("@LinkedIn", model.LinkedIn);                   
                   updateinstructors.AddWithValue("@CoursesTaught", String.Join("|", model.CoursesTaught));
               });
        }

        internal object GetInstructors()
        {
            throw new NotImplementedException();
        }

        public List<Instructors> GetInstructors(List<int> ids)
        {
            List<Instructors> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo. Instructors_SelectById",
            inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Structured);

                if (ids != null && ids.Any())
                {
                    p.Value = new Sabio.Data.IntIdTable(ids);
                }
                paramCollection.Add(p);
            }, map: delegate(IDataReader reader, short set)
            {
                Instructors p = new Instructors();
                int startingIndex = 0; //startingOrdinal
                p.Id = reader.GetSafeInt32(startingIndex++);
                p.Name = reader.GetSafeString(startingIndex++);
                p.Email = reader.GetSafeString(startingIndex++);
                p.Bio = reader.GetSafeString(startingIndex++);
                p.LinkedIn = reader.GetSafeString(startingIndex++);
                p.CoursesTaught = reader.GetSafeString(startingIndex++);

                if (list == null)
                {
                    list = new List<Instructors>();
                }

                list.Add(p);



            }
            );

            return list;

        }

        public Instructors GetInstructors(int id)
        {
            Instructors item = null;

            CourseBase bc = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Instructors_SelectByIdV2"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    //model binding

                }, map: delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        item = new Instructors();
                        int startingIndex = 0; // startingOrdinal

                        item.Id = reader.GetSafeInt32(startingIndex++);
                        item.Name = reader.GetSafeString(startingIndex++);
                        item.Email = reader.GetSafeString(startingIndex++);
                        item.Bio = reader.GetSafeString(startingIndex++);
                        item.LinkedIn = reader.GetSafeString(startingIndex++);
                        item.CoursesTaught = reader.GetSafeString(startingIndex++);
                        //bc.Id = reader.GetSafeInt32(startingIndex++);
                        //bc.CourseName = reader.GetSafeString(startingIndex++);
                    }
                    else if (set == 1)
                    {
                        bc = new CourseBase();
                        int startingIndex = 0;

                        bc.Id = reader.GetSafeInt32(startingIndex++);
                        bc.CourseName = reader.GetSafeString(startingIndex++);

                        if (item.Courses == null)
                        {
                            item.Courses = new List<CourseBase>();
                        }
                        item.Courses.Add(bc);
                    }
                }
                );
            return item;



        }

        public List<Instructors> Instructors()
        {
            List<Instructors> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Instructors_SelectAll"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   Instructors In = new Instructors();
                   int startingIndex = 0;

                   In.Id = reader.GetSafeInt32(startingIndex++);
                   In.Name = reader.GetSafeString(startingIndex++);
                   In.Email = reader.GetSafeString(startingIndex++);
                   In.Bio = reader.GetSafeString(startingIndex++);
                   In.LinkedIn = reader.GetSafeString(startingIndex++);
                   In.CoursesTaught = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Instructors>();
                   }

                   list.Add(In);
               }
               );
            return list;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Instructors_DeleteById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                });
        }

    }
}
