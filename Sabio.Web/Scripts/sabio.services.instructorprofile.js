if (!sabio.services) {
    sabio.services = {};
}

if (!sabio.services.instructor) {
    sabio.services.instructor = {};
}

sabio.services.instructor.getInstructor = function (id, onSuccess, onError) {

    var url = "/api/instructors/" + id;

    var settings = {

        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError

    };

    $.ajax(url, settings);
}