using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IAttendanceService
    {
        Attendance Add(AttendanceAddRequest model, string userId);

        List<Attendance> getAttendanceByCampus(int id);

        List<Attendance> getAttendanceByCheckInId(int id);

        List<Attendance> getAttendanceByDay(int id, DateTime date);

        //List<Attendance> getAttendanceByUser(string UserId);

        List<Attendance> getAll();

        List<Attendance> getAllCheckIn();

        List<Attendance> GetAllNearby(int id);

        List<Attendance> getAttendanceByUser(int id);

        List<Campus> GetCampuses();
    }
}
