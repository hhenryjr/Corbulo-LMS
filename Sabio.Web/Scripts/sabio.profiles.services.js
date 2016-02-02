if (!sabio.profiles) {
    sabio.profiles = { services: { users: {} } };
}

sabio.profiles.services.users.getUserInfoByUserId = function (onSuccess, onError) {

    var url = "/api/user/current";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError

    };

    $.ajax(url, settings);
}

sabio.profiles.services.users.getCoursesByUserId = function (onSuccess, onError) {

    //var id = $("#id").val();
    var url = "/api/courses/user";

    var setting = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, setting);
}

sabio.profiles.services.users.saveBackGroundPicture = function (myFormData, fileType, onSuccess, onError) {

    var url = "/api/files/" + fileType

    var setting = {
        contentType: false,
        type: "POST",

        data: myFormData,

        dataType: "json",
        success: onSuccess,
        error: onError,

        cache: false,
        processData: false
    };

    $.ajax(url, setting);
}


sabio.profiles.services.users.saveAvatarPicture = function (myFormData, fileType, onSuccess, onError) {

    var url = "/api/files/" + fileType

    var setting = {
        contentType: false,
        type: "POST",

        data: myFormData,

        dataType: "json",
        success: onSuccess,
        error: onError,

        cache: false,
        processData: false
    };

    $.ajax(url, setting);
}
sabio.profiles.services.users.GetSectionDetailsByUserId = function (onSuccess, onError) {

    var url = "/api/user/sectiondetails";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError

    };

    $.ajax(url, settings);
}


sabio.profiles.services.users.GetSectionInstructors = function (id, onSuccess, onError) {

    var url = "/api/admin/sections/Instructor/" + id;

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError

    };

    $.ajax(url, settings);
}

sabio.profiles.services.users.getAddressById = function (id, onSuccess, onError) {

    var url = "/api/Addresses/" + id;      

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