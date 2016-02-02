using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IBlogService
    {
        int InsertBlog(BlogAddRequest model);

        void UpdateBlog(BlogUpdateRequest model);

        Blog Get(int id);

        Blog Get(string tagName);

        List<Blog> GetBlogList();

        List<Blog> GetBlogsByTagName(string tagName);

        List<BlogTag> getTagsUsedByBlogs();

        void Delete(int id);
    }
}
