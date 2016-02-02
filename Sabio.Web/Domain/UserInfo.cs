using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        //public int AddressId { get; set; }

        public int Gender { get; set; }

        public string UserId { get; set; }

        public string Bio { get; set; }

        //public string Facebook { get; set; }

        //public string LinkedIn { get; set; }

        //public string Twitter { get; set; }

        //public string Webpage { get; set; }

        public UserTracks UserTracks { get; set; }

        public Campus Campuses { get; set; }

        public Track Track { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string CoverPhotoPath { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string AvatarPhotoUrl { get; set; }

        public int DesiredTrack { get; set; }

        public int DesiredCampusLocation { get; set; }

        public int OnboardStatus { get; set; }

        public DateTime DesiredSabioStartDate { get; set; }

    }
}