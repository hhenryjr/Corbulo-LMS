//SEPARATION OF CONCERNS

if (!sabio.services.tracks) {
    sabio.services.tracks = {};
}

//Calls from Index View
//AJAX Call POST               C
sabio.services.tracks.add = function (trackData, onSuccess, onError) {
   
    var url = "/api/Tracks/";
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: trackData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}


//AJAX Call GET                R
sabio.services.tracks.get = function (id, onSuccess, onError) {

    var url = "/api/Tracks/Get/" + id;
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


//AJAX Call PUT                U
sabio.services.tracks.update = function (id, myData, onSuccess, onError) {
    
    var url = "/api/Tracks/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}


//AJAX Call DELETE             D
sabio.services.tracks.delete = function (id, onSuccess, onError) {
   
    alert("Record #" + " " + id + " " + "has been deleted");
    var url = "/api/Tracks/" + id;

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


//Calls from List View
//AJAX Call //Get tracks
sabio.services.tracks.getTracks = function (onSuccess, onError) {

    var url = "/api/Tracks/";
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

sabio.services.tracks.getCourseById = function (id, onSuccess, onError) {

    //var id = $("#id").val();
    var url = "/api/courses/" + id;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, setting);


}

sabio.services.tracks.getAllCourses = function ( onSuccess, onError) {

    var url = "/api/courses" 
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

sabio.services.tracks.getSectionByCourseId = function (id, onSuccess, onError) {

    //var id = $("#id").val();
    var url = "/api/tracks/courseid/" + id;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, setting);


}

sabio.services.tracks.getAllSectionsByCourseId = function (id, onSuccess, onError) {

    //var id = $("#id").val();
    var url = "api/admin/sections/list/" + id;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, setting);


}


sabio.services.tracks.getUserSectionInfoByUserId = function ( onSuccess, onError) {

    //var id = $("#id").val();
    var url = "api/admin/sections/list/user";

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, setting);
}



sabio.services.tracks.requestEnrollmentForThisSection = function (myData, onSuccess, onError) {

    //var id = $("#id").val();
    var url = "api/admin/sections/register/user";

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: myData
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, setting);
}