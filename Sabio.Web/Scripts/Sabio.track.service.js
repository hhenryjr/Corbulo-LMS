
if (!sabio.track) {
    sabio.track = { services: { tracksList: {} } };
}


sabio.track.services.tracksList.get = function (onSuccess, onError) {

    var url = "/api/tracks";

    var setting = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError
    }
    $.ajax(url, setting);
}