using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.CoursesRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ICoursesService
    {
        int Add(AddRequest model, string userId);

        void Update(CourseUpdateRequest model);

        Course Get(int id);

        List<Course> Get();

        void Delete(int id);

        List<Course> GetByUserId(string userId);

        void AddCourseInstructor(int cId, int Id);

        void DeleteCourseInstructor(int cId, int Id);

        void AddCourseTags(int id, int tagId);

        void DeleteCourseTag(int id, int tagId);

        void AddCoursePrereqs(int id, int preqId);

        void DeleteCoursePrereqs(int id, int preqId);

        void UpdateCourse(TrackCourseUpdate model, int Id);

    }
}
