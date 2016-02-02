using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Sections
{
    public class SectionLocation
    {
        [Required]
        public int CampusLocation { get; set; }

        [Required]
        public string RoomNumber { get; set; }
    }
}