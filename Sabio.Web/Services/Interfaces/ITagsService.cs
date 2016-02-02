using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ITagsService
    {
        int Add(TagsAddRequest model, string userId);

        void Update(TagsUpdateRequest model);

        void UpdateActive(TagsUpdateActiveRequest model);

        Tag Get(int id);

        List<Tag> Get();


    }
}