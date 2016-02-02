//if (!sabio.services) {
//    sabio.services = {};
//}

if (!sabio.services.dashboardSidebar) {
    sabio.services.dashboardSidebar = {};
}

sabio.services.dashboardSidebar.getAll = function (onSuccess, onError) {

    var url = "/api/Wiki/pages";
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

// gets space for dropdown
sabio.services.dashboardSidebar.getAllSpaces = function (onSuccess, onError) {

    var url = "/api/Wiki/spaces";
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

sabio.services.dashboardSidebar.getSpaceById = function (id, onSuccess, onError) {

    var url = "/api/Wiki/spaces/" + id + "/pages";
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

sabio.services.dashboardSidebar.addLocation = function (attendanceData, onSuccess, onError) {

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