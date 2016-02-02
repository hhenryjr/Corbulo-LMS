using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ITrackCourseService
    {
        TrackCourse Get(int id);

        int Insert(TrackCourseRequest model);

        void Update(TrackCourseUpdate model);

        void Delete(int id);

        List<TrackCourse> GetByTrack(int id);


    }
}