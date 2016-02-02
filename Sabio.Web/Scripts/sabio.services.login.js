//SEPARATION OF CONCERNS

if (!sabio.services.login) {
    sabio.services.login = {};
}

//Calls from Index View
//AJAX Call POST               C
sabio.services.login.add = function (loginData, onSuccess, onError) {

    var url = "/api/user/login";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: loginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}

sabio.services.login.reset = function (tokenData, onSuccess, onError) {

    var url = "/api/user/resetpassword";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: tokenData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}


