if (!sabio.services) {
    sabio.services = {};
}

if (!sabio.services.userSettings) {
    sabio.services.userSettings = {};
}

sabio.services.userSettings.get = function (onSuccess, onError) {

    var url = "/api/user/settings";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError

    };

    $.ajax(url, settings);
}

sabio.services.userSettings.put = function (data, onSuccess, onError) {
    
    var url = "/api/user";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}