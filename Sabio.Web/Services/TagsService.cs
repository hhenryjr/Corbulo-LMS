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
using Sabio.Web.Models.Requests.Tags;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class TagsService : BaseService, ITagsService
    {
        public int Add(TagsAddRequest model, string userId)
        {
             var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tags_Insert",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {

                    parameterCollection.AddWithValue("@TagName", model.TagName);
                    parameterCollection.AddWithValue("@Approved", model.Approved);
                    parameterCollection.AddWithValue("@UserId", userId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterCollection.Add(p);
                }, returnParameters: delegate (SqlParameterCollection para)
                {
                    int.TryParse(para["@Id"].Value.ToString(), out id);
                }
                );

            return id;
        }

        public void Update(TagsUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tags_Update",
                inputParamMapper: delegate (SqlParameterCollection updateTags)
                {
                    updateTags.AddWithValue("@Id", model.Id);
                    updateTags.AddWithValue("@TagName", model.TagName);
                    updateTags.AddWithValue("@Approved", model.Approved);

                });
        }

        public void UpdateActive(TagsUpdateActiveRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Tags_UpdateApproved",
                inputParamMapper: delegate (SqlParameterCollection updateTags)
                {
                    updateTags.AddWithValue("@Id", model.Id);
                    updateTags.AddWithValue("@Approved", model.Approved);
                });
        }

        public Tag Get(int id)
        {
            Tag item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Tags_SelectById",
            //DataProvider.ExecuteCmd(GetConnection, "dbo.CourseTags_SelectByCourseId",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    item = MapTag(reader);


                }
                );
            return item;
        }

        public List<Tag> Get()
        {
            List<Tag> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Tags_SelectAll",
                inputParamMapper: null, map: delegate (IDataReader reader, short set)
            {

                Tag item = MapTag(reader);

                if (list == null)
                {
                    list = new List<Tag>();
                }

            list.Add(item);

            }
            );
            return list;
        }

        private static Tag MapTag(IDataReader reader)
        {
            Tag item = new Tag();
            int startingIndex = 0;

            item.Id = reader.GetSafeInt32(startingIndex++);
            item.TagName = reader.GetSafeString(startingIndex++);
            item.Approved = reader.GetSafeBool(startingIndex++);
            return item;
        }
    }
}