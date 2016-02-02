if (!sabio.services.officeHours) {
    sabio.services.officeHours = {};
}



sabio.services.officeHours.getSectionsList = function (onSuccess, onError) {

    var url = "/api/admin/sections/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}



sabio.services.officeHours.submitOfficeHoursForm = function (myData, onSuccess, onError) {

    var url = "/api/OfficeHour/Add/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}



sabio.services.officeHours.updateOfficeHoursForm = function (myData, id, onSuccess, onError) {

    var url = "/api/OfficeHour/" + id + "/Edit";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "PUT",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}



sabio.services.officeHours.getOfficeHourSession = function (id, onSuccess, onError) {

    var url = "/api/OfficeHour/" + id ;

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
       
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}


sabio.services.officeHours.getOfficeHourList = function (onSuccess, onError) {

    var url = "/api/OfficeHour/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",

        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}



sabio.services.officeHours.deleteOfficeHourSession = function (id, onSuccess, onError) {

    var url = "/api/OfficeHour/" + id + "/Delete";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "DELETE",

        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}

sabio.services.officeHours.getByDate = function (onSuccess, onError) {

    var url = "/api/User/OfficeHours/";

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}


sabio.services.officeHours.getByOfficeHourIdAndQuestionId = function (oId, Id, onSuccess, onError) {

    var url = "/api/OfficeHour/" + oId  + "/question/" + Id;

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}

sabio.services.officeHours.addResponse = function (oId, Id, myData, onSuccess, onError) {

    var url = "/api/OfficeHour/" + oId + "/question/" + Id;

    var settings = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: myData,
        type: "PUT",
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}

sabio.services.officeHours.deleteQuestion = function (id, onSuccess, onError) {

    var url = "/api/OfficeHour/" + id + "/deleteQuestion";
    var settings = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "DELETE",
        dataType: "json",
        success: onSuccess,
        error: onError
    };
    $.ajax(url, settings);
}

sabio.services.officeHours.getByOfficeHourId = function(id, onSuccess, onError)
{   
    var url = "/api/questions/" + id;

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET", 
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}

sabio.services.officeHours.AddQuestion = function (id,myData, onSuccess, onError) {

    var url = "/api/questions/add/"+id;

    var settings = {
        
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "POST",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError
    };

    $.ajax(url, settings);
}
