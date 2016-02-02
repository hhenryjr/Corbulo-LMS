// check if there is an object

if (!sabio.services.confirmEmail) {
    sabio.services.confirmEmail = {};
}

// http:Post
sabio.services.confirmEmail = function (myData, onSuccess, onError) {
    var url = "/api/user/ConfirmEmail/" + myData;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
        , async: true
    };
    $.ajax(url, settings);
}




