using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Tests;
using Sabio.Web;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class CommentsServices : BaseService, ICommentsService
    {
        public int Insert(CommentAddRequest model, string userId)
        {
            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comments_Insert"
                , inputParamMapper: delegate(SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@UserId", userId);
                    commentsCollection.AddWithValue("@OwnerId", model.OwnerId);
                    commentsCollection.AddWithValue("@OwnerTypeId", model.OwnerTypeId);
                    commentsCollection.AddWithValue("@Title", model.Title);
                    commentsCollection.AddWithValue("@Body", model.Body);
                    commentsCollection.AddWithValue("@ParentId", model.ParentId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    commentsCollection.Add(p);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }

        public void Update(CommentUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comments_Update"
                , inputParamMapper: delegate(SqlParameterCollection UpdateCommentsCollection)
                {
                    UpdateCommentsCollection.AddWithValue("@Id", model.Id);
                    UpdateCommentsCollection.AddWithValue("@OwnerId", model.OwnerId);
                    UpdateCommentsCollection.AddWithValue("@OwnerTypeId", model.OwnerTypeId);
                    UpdateCommentsCollection.AddWithValue("@Title", model.Title);
                    UpdateCommentsCollection.AddWithValue("@Body", model.Body);
                    UpdateCommentsCollection.AddWithValue("@ParentId", model.ParentId);
                });
        }

        public Comment Get(int id)
        {
            Comment item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Comments_SelectById"
                , inputParamMapper: delegate(SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@Id", id);
                }, map: delegate(IDataReader reader, short set)
                {
                    item = MapComments(reader);
                });
            return item;
        }

        private static Comment MapComments(IDataReader reader)
        {
            Comment item = new Comment();
            int startingIndex = 0;

            item.Id = reader.GetSafeInt32(startingIndex++);
            item.UserId = reader.GetSafeString(startingIndex++);
            item.OwnerId = reader.GetSafeInt32(startingIndex++);

            item.OwnerTypeId = reader.GetSafeInt32(startingIndex++);
            item.Title = reader.GetSafeString(startingIndex++);

            item.Body = reader.GetString(startingIndex++);
            item.ParentId = reader.GetSafeInt32(startingIndex++);

            item.DateAdded = reader.GetDateTime(startingIndex++);
            item.DateModified = reader.GetDateTime(startingIndex++);
            item.AvatarPhotoUrl = reader.GetSafeString(startingIndex++);
            item.FirstName = reader.GetSafeString(startingIndex++);
            item.LastName = reader.GetSafeString(startingIndex++);

            return item;
        }

        public List<Comment> GetList(int ownerId, int ownerTypeId, int parentId)
        {
            List<Comment> list = null;

            DataProvider.ExecuteCmd(GetConnection, "Comments_SelectByOwnerIdOwnerTypeIdParentId"
                , inputParamMapper: delegate(SqlParameterCollection commentsCollection)
            {
                commentsCollection.AddWithValue("@OwnerId", ownerId);
                commentsCollection.AddWithValue("@OwnerTypeId", ownerTypeId);
                commentsCollection.AddWithValue("@ParentId", parentId);
            }, map: delegate(IDataReader reader, short set)
                {
                    Comment item = MapComments(reader);

                    if (list == null)
                    {
                        list = new List<Comment>();
                    }

                    list.Add(item);
                });
            return list;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comments_DeleteById"
                , inputParamMapper: delegate(SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@Id", id);
                });
        }
    }
}