// check if there is an object

if (!sabio.services.userTokens) {
    sabio.services.userTokens = {};
}

// http:Post
sabio.services.userTokens.add = function (tokenData, onSuccess, onError) {
    var url = "/api/user/UserTokens";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: tokenData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}

sabio.services.userTokens.verifyToken = function (tokenData, onSuccess, onError) {
    var url = "/api/user/UserTokens/IsValid/" + tokenData;
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

sabio.services.userTokens.DeleteToken = function (tokenData, onSuccess, onError) {
    var url = "/api/user/UserTokens/" + tokenData;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };
    $.ajax(url, settings);
}

sabio.services.userTokens.getTokenUserName = function (tokenData, onSuccess, onError) {
    var url = "/api/user/UserTokens/" + tokenData;
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