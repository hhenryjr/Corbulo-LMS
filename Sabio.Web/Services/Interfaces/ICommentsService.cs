using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ICommentsService
    {
        int Insert(CommentAddRequest model, string userId);

        void Update(CommentUpdateRequest model);

        Comment Get(int id);

        List<Comment> GetList(int ownerId, int ownerTypeId, int parentId);

        void Delete(int id);
    }
}
