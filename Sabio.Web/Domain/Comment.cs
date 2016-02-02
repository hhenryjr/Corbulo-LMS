using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Comment
    {
        public int Id { get; set; }
 
        public string UserId { get; set; }
       
        public int OwnerId { get; set; }

        public int OwnerTypeId { get; set; }

        public string Title { get; set; }
  
        public string Body { get; set; }

        public int ParentId { get; set; }

        public string UserName { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public string AvatarPhotoUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}