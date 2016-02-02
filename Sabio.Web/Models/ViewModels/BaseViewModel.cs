using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewModel
    {
        //Sabio: make note that this base class does not have to be, or should not be abstract. 
        // it is a perfectly good class to be used as a ViewModel 
        
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public IdentityUser CurrentUser { get; set; }
        public UserInfo UserProfile { get; set; }
        public bool HasWikiTreeNavigation { get; set; } // indicates if main nav bar should be by wiki tree
        public string BrandName { get; set; }
        public string BrandTagline { get; set; }
        public string BrandLogo { get; set; }
        public string BrandDescription { get; set; }
        //public string /*UserInfo*/ FirstName { get; set; }
        //public string /*UserInfo*/ LastName { get; set; }
        //public string /*UserInfo*/ AvatarPhotoId { get; set; }

    }
}