using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Data;
using System.Data.SqlClient;
using Sabio.Web.Models.Requests.Blogs;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Domain;
using System.Data;
using Sabio.Web.Controllers.Api.Blogs;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class BlogService : BaseService, IBlogService
    {
        public int InsertBlog(BlogAddRequest model)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blogs_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Title", model.Title );
                   paramCollection.AddWithValue("@DatePublished", model.PublishedDate);
                   paramCollection.AddWithValue("@BlogPost", model.BlogPost);
                   paramCollection.AddWithValue("@Tags", model.Tags);
                   paramCollection.AddWithValue("@IsFeatured", model.IsFeatured);


                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               },
               returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out id);
               }
               );


            return id;
        }

        public void UpdateBlog(BlogUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blogs_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@DatePublished", model.PublishedDate);
                   paramCollection.AddWithValue("@BlogPost", model.BlogPost);
                   paramCollection.AddWithValue("@Tags", model.Tags);
                   paramCollection.AddWithValue("@IsFeatured", model.IsFeatured);
                   paramCollection.AddWithValue("@Id", model.Id);                
               });
          
        }

        public Blog Get(int id)
        {
            Blog blog = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Blogs_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
                   //model binding
               }, map: delegate (IDataReader reader, short set)
               {
                   if (set == 0)
                   {
                       blog = MapBlog(reader);
                   }
                   else if (set == 1)
                   {
                       BlogTag bt = MapBlogTag(reader);
                       if (blog.Tags == null)
                       {
                           blog.Tags = new List<BlogTag>();
                       }
                       blog.Tags.Add(bt);
                   }

               }
               );
            return blog;
        }

        public Blog Get(string tagName)
        {
            Blog blog = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Blogs_SelectByTagName"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@tagName", tagName);
                   //model binding
               }, map: delegate (IDataReader reader, short set)
               {
                   if (set == 0)
                   {
                       blog = MapBlog(reader);
                   }
                   else if (set == 1)
                   {
                       BlogTag bt = MapBlogTag(reader);
                       if (blog.Tags == null)
                       {
                           blog.Tags = new List<BlogTag>();
                       }
                       blog.Tags.Add(bt);
                   }

               }
               );
            return blog;
        }

        private static Blog MapBlog(IDataReader reader)
        {
            Blog blog = new Blog();
                   int startingIndex = 0; //startingOrdinal

            blog.Id = reader.GetSafeInt32(startingIndex++);
            blog.Title = reader.GetSafeString(startingIndex++);
            blog.DatePublished = reader.GetDateTime(startingIndex++);
            blog.BlogPost = reader.GetSafeString(startingIndex++);
            //blog.DateAdded = reader.GetDateTime(startingIndex++);
            //blog.DateModified = reader.GetDateTime(startingIndex++);
            return blog;

               }

        private static BlogTag MapBlogTag(IDataReader reader)
        {
            BlogTag item = new BlogTag();
            int startingIndex = 0;

            item.BlogId = reader.GetSafeInt32(startingIndex++);
            item.TagId = reader.GetSafeInt32(startingIndex++);
            item.TagName = reader.GetSafeString(startingIndex++);

            return item;
        }

        public List<Blog> GetBlogList()
        {
            List<Blog> list = null;
            Dictionary<int, Blog> dictionary = null;


            DataProvider.ExecuteCmd(GetConnection, "dbo.Blogs_SelectAll_V2"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)

               {

                   if (set == 0)
                   {
                       if (list == null)
                       {
                           list = new List<Blog>();

                       }
                       if (dictionary == null)
               {
                           dictionary = new Dictionary<int, Blog>();

                       }

                       Blog b = MapBlog(reader);
                       list.Add(b);

                       dictionary.Add(b.Id, b);
                   }
                   else if (set == 1)
                   {
                       BlogTag bt = MapBlogTag(reader);
                       Blog parentBlog = dictionary[bt.BlogId];
                       if (parentBlog.Tags == null)
                       {
                           parentBlog.Tags = new List<BlogTag>();
                       }

                       parentBlog.Tags.Add(bt);

                   }

               }
               );

            return list;
        }

        public List<Blog> GetBlogsByTagName(string tagName)
        {
            List<Blog> list = null;
            Dictionary<int, Blog> dictionary = null;


            DataProvider.ExecuteCmd(GetConnection, "dbo.Blogs_SelectByTagName"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@tagName", tagName);
                   //model binding
               }, map: delegate (IDataReader reader, short set)

               {

                   if (set == 0)
                   {
                       if (list == null)
                       {
                           list = new List<Blog>();

                       }
                       if (dictionary == null)
                       {
                           dictionary = new Dictionary<int, Blog>();

                       }

                       Blog b = MapBlog(reader);
                       list.Add(b);

                       dictionary.Add(b.Id, b);
                   }
                   else if (set == 1)
                   {
                       BlogTag bt = MapBlogTag(reader);
                       Blog parentBlog = dictionary[bt.BlogId];
                       if (parentBlog.Tags == null)
                       {
                           parentBlog.Tags = new List<BlogTag>();
                       }

                       parentBlog.Tags.Add(bt);

                   }

               }
               );

            return list;
        }

        public List<BlogTag> getTagsUsedByBlogs()
        {
            List<BlogTag> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Tags_SelectUsedByBlogs",
                inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {

                    BlogTag item = MapTagsUsedByBlogs(reader);

                   if (list == null)
                   {
                        list = new List<BlogTag>();
                   }

                    list.Add(item);

               }
               );
            return list;
        }

        private static BlogTag MapTagsUsedByBlogs(IDataReader reader)
        {
            BlogTag item = new BlogTag();
            int startingIndex = 0;

            
            item.TagId = reader.GetSafeInt32(startingIndex++);
            item.TagName = reader.GetSafeString(startingIndex++);

            return item;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blogs_DeleteById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                });
        }
    }
}


