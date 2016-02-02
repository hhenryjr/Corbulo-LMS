using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface ITrackService
    {
        int Add(TrackAddRequest model);

        void Update(TrackUpdateRequest model);

        void Delete(int id);

        Track Get(int id);

        List<Track> GetTrackList();

        CourseSection GetSection(int id);

        int Get();
    }
}