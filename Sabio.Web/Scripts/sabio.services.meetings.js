if (!sabio.services.meetings)
{
    sabio.services.meetings = {};
}

sabio.services.meetingsFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.meeetings;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.services.meetings.add = function (meetingData, onSuccess, onError) {
    var url = "/api/Meetings";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: meetingData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}

sabio.services.meetings.update = function (meetingData, onSuccess, onError) {
    var url = "/api/Meetings";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: meetingData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}

sabio.services.meetings.getById = function (meetingId, onSuccess, onError) {
    var url = "/api/Meetings/" + meetingId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

sabio.services.meetings.getAll = function (onSuccess, onError) {
    var url = "/api/Meetings";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};
