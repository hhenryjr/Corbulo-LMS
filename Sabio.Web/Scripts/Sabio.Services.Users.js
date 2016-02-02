if (!sabio.services.users) {

    sabio.services.users = {};
}

// AJAX POST  
sabio.services.users.Add = function (userData, onSucess, onError) {

    var url = "/api/user/profile";
    
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: userData,
        dataType: "json",
        success: onSucess,
        error: onError,
        type: "POST"
    };
    $.ajax(url, setting);
};

// AJAX PUT
sabio.services.users.update = function (id, userInfo, onSucess, onError) {

    var url = "/api/user/profile/" + id;

    var userData = "id=" + id + "&" + userInfo;

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: userData,
        success: onSucess,
        dataType: "json",
        type: "PUT",
        error: onError

    };
    $.ajax(url, setting);
};

// AJAX GET

sabio.services.users.get = function (id, onSucess, onError) {

    var url = "/api/user/profile/" + id;

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSucess,
        error: onError,
        type: "GET"

    };
    $.ajax(url, setting);
};

// AJAX DELETE

sabio.services.users.delete = function (onSuccess, onError) {

    var id = $("#UserInfoId").val();
    var url = "/api/user/delete/" + id;
    var setting = {
        cahce: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "DELETE"
    }
    $.ajax(url, setting);
};

//AJAX GET LIST
sabio.services.users.getArray = function (onSuccess, onErrorr) {

    var url = "/api/user/list";
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onErrorr,
        type: "GET"

    };
    $.ajax(url, setting);
}


sabio.services.users.getOnboardedStudents = function (onSuccess, onErrorr) {

    var url = "/api/user/onboardedList";
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onErrorr,
        type: "GET"

    };
    $.ajax(url, setting);
}