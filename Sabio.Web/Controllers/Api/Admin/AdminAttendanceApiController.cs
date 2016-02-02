using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;

namespace Sabio.Web.Controllers.Api.Admin
{

    [RoutePrefix("api/admin/attendance")]
    public class AdminAttendanceApiController : BaseAdminApiController
    {

        private IAttendanceService _attendanceService;

        public AdminAttendanceApiController(IAttendanceService AttendanceService)
        {
            _attendanceService = AttendanceService;
        }


        [AllowAnonymous]
        [Route("Add"), HttpPost]
        public HttpResponseMessage Add(AttendanceAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();
            string userId = UserService.GetCurrentUserId();
            _attendanceService.Add(model, userId);

            return Request.CreateResponse(response);
        }

        [Route("campus/{id:int}"), HttpGet]
        public HttpResponseMessage GetInfoByCampus(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAttendanceByCampus(id);

            return Request.CreateResponse(response);
        }

        [Route("checkIn/{id:int}"), HttpGet]
        public HttpResponseMessage GetInfoByCheckInId(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAttendanceByCheckInId(id);

            return Request.CreateResponse(response);
        }

        [Route("{id:int}/date/{date:DateTime}"), HttpGet]
        public HttpResponseMessage GetInfoDay(int id, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAttendanceByDay(id, date);

            return Request.CreateResponse(response);

        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetInfoUser(int Id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAttendanceByUser(Id);

            return Request.CreateResponse(response);

        }

        // GET Attendance (LIST)
        [Route("list"), HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAll();

            return Request.CreateResponse(response);
        }

        // GET CHECKEDIN (LIST)
        [Route("checkedInList"), HttpGet]
        public HttpResponseMessage getAllCheckIn()
        {
            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.getAllCheckIn();

            return Request.CreateResponse(response);
        }

        [Route("nearby/{id:int}"), HttpGet]
        public HttpResponseMessage GetAllNearby(int id)
        {
            ItemsResponse<Attendance> response = new ItemsResponse<Attendance>();

            response.Items = _attendanceService.GetAllNearby(id);

            return Request.CreateResponse(response);
        }

        //GET CAMPUS LIST
        [Route("campuses"), HttpGet]
        public HttpResponseMessage GetCampuses()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<Campus> response = new ItemsResponse<Campus>();

            response.Items = _attendanceService.GetCampuses();

            return Request.CreateResponse(response);
        }
    }
}
