using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Testimonials;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class TestimonialsService : BaseService, ITestimonialsService
    {
        public int Get()
        {
            return 0;
        }

        public int Insert(TestimonialsAddRequest model)
        {
            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Testimonial_Insert",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Title", model.Title);
                    parameterCollection.AddWithValue("@Body", model.Body);
                    parameterCollection.AddWithValue("@StarRating", model.StarRating);
                    SqlParameter s = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterCollection.Add(s);
                },
                returnParameters: delegate(SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                );
            return id;
        }

        public Testimonials Get(int id)
        {
            Testimonials t = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Testimonial_SelectById",
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                },
                map: delegate(IDataReader reader, short set)
                {
                    t = TestimonialMap(reader);
                }
                );
            return t;
        }

        public List<Testimonials> GetList()
        {
            List<Testimonials> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Testimonial_SelectAll"
                , inputParamMapper: null, map: delegate(IDataReader reader, short set)
                {
                    Testimonials item = TestimonialMap(reader);
                    if (list == null)
                    {
                        list = new List<Testimonials>();
                    }
                    list.Add(item);
                });

            return list;
        }

        public void Update(TestimonialsUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Testimonial_Update",
           inputParamMapper: delegate(SqlParameterCollection t)
           {
               t.AddWithValue("@Id", model.Id);
               t.AddWithValue("@Title", model.Title);
               t.AddWithValue("@Body", model.Body);
               t.AddWithValue("@StarRating", model.@StarRating);
           });
        }

        private Testimonials TestimonialMap(IDataReader reader)
        {
            Testimonials t = new Testimonials();
            int startingIndex = 0;
            t.Id = reader.GetSafeInt32(startingIndex++);
            t.Title = reader.GetSafeString(startingIndex++);
            t.Body = reader.GetSafeString(startingIndex++);
            t.StarRating = reader.GetSafeInt32(startingIndex++);

            return t;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Testimonial_DeleteById",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
        }
    }
}