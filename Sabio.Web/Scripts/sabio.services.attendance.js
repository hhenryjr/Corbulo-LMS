if (!sabio.services.attendance) {
    sabio.services.attendance = {};
}

sabio.services.attendance.addLocation = function (attendanceData, onSuccess, onError) {

    var url = "/api/user/attendance";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: attendanceData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}

    //AJAX Call GET Attendance            
    sabio.services.attendance.getAttendance = function (onSuccess, onError) {

        var url = "/api/admin/attendance/list/";
        var settings = {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };

    $.ajax(url, settings);

    }

 //AJAX Call GET CHECKEDIN            
    sabio.services.attendance.getAllCheckedIn = function (onSuccess, onError) {

        var url = "/api/admin/attendance/checkedInList/";
        var settings = {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };

    $.ajax(url, settings);

}

    sabio.services.attendance.getNearbyAttendance = function (id, onSuccess, onError) {

        var url = "/api/admin/attendance/nearby/" + id;
        var settings = {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };

        $.ajax(url, settings);

    }
    sabio.services.attendance.getAttendanceByUser = function (id, onSuccess, onError) {

        var url = "/api/admin/attendance/" + id;
        var settings = {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };

        $.ajax(url, settings);

    }

    //AJAX Call GET Campuses            
    sabio.services.attendance.getCampuses = function (onSuccess, onError) {

        var url = "/api/admin/attendance/campuses"
        var settings = {
            cache: false
            , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            , dataType: "json"
            , success: onSuccess
            , error: onError
            , type: "GET"
        };

        $.ajax(url, settings);

    }



