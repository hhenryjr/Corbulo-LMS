
if (!sabio.services.roles) {
    sabio.services.roles = {};
}
//AJAX GET LIST
sabio.services.roles.getRoles = function (onSuccess, onErrorr) {

    var url = "/admin/users/Roles";
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

sabio.services.roles.getNetUsers = function (onSuccess, onErrorr) {

    var url = "/admin/users/All";
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

sabio.services.roles.getUserRoleById = function(id, onSuccess, onErrorr) {
    var url = "/admin/users/" + id;

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

sabio.services.roles.addRoles = function (id, roleId, onSuccess, onError) {

    var url = "/admin/users/"+ id +"/role/"+ roleId;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}

sabio.services.roles.deleteRole = function (id,roleId, onSuccess, onError) {
    var url = "/admin/users/" + id + "/role/" + roleId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "Delete"
    };
    $.ajax(url, settings);
}

sabio.services.roles.getRoleByUserId = function (id, onSuccess, onErrorr) {
    var url = "/admin/users/" + id;

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