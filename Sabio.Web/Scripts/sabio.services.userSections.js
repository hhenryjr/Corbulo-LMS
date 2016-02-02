
if (!sabio.services.userSections) {
    sabio.services.userSections = {};
}

// AJAX PUT
sabio.services.userSections.updateUserStatus = function (id, minions, onSuccess, onError) {
   
    var url = "/admin/users/UserStatus/" + id;
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: minions,
        success: onSuccess,
        dataType: "json",
        type: "PUT",
        error: onError
    };
    $.ajax(url, setting);
};

// AJAX GET

sabio.services.userSections.getUsersDetail = function (id, onSuccess, onError) {

    var url = "/admin/users/detail/" + id;

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"

    };
    $.ajax(url, setting);
};


////GET ALL SECTIONS
sabio.services.userSections.getSections = function (onSuccess, onError) {

    var url = "/api/sections/list"
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

//AJAX GET LIST
sabio.services.userSections.getUserList= function (onSuccess, onErrorr) {

    var url = "/admin/users/Userlist";
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
