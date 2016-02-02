using System;

namespace Sabio.Web.Domain
{
    public class BlogItem
    {
        public string BlogPost { get; internal set; }
        public DateTime DateAdded { get; internal set; }
        public DateTime DateModified { get; internal set; }
        public DateTime DatePublished { get; internal set; }
        public int Id { get; internal set; }
        public string Tags { get; internal set; }
        public bool IsFeatured { get; set; }
        public string Title { get; internal set; }
    }
}