using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Attendance
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string AvatarPhotoUrl { get; set; }

        public int CampusId { get; set; }

        public string CampusName { get; set; }

        public string UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public bool CheckedIn { get; set; }

        public double? DistanceInMeters { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int  EnrollmentStatusID { get; set; }
    
    }
}