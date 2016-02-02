// check if there is an object

if (!sabio.services.registration) {
    sabio.services.registration = {};
}

// http:Post
sabio.services.registration.add = function (myData, onSuccess, onError) {
    var url = "/api/user/Register";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}
