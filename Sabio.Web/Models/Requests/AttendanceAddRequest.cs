using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AttendanceAddRequest
    {
        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }
    }
}