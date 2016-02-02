using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests.Track
{
    public class TrackCourseRequest
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Order { get; set; }
    }
}