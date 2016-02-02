
if (!sabio.services.sections) {
    sabio.services.sections = {};
}

sabio.services.sectionsFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.sections;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

//Insert A New Section
sabio.services.sections.addSection = function (sectionData, onSuccess, onError) {

    var url = "/api/admin/sections";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: sectionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}

////Get A Section 
sabio.services.sections.getSection = function (id, onSuccess, onError) {

    var url = "/api/admin/sections/" + id;
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

////Update A Section              U
sabio.services.sections.updateSection = function (id, myData, onSuccess, onError) {

    var url = "/api/admin/sections/" + id;

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

////AJAX Call DELETE A SECTION            D
sabio.services.sections.deleteSection = function (id, onSuccess, onError) {

    
    var url = "/api/admin/sections/" + id;

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


//AJAX Call GET Campuses            
sabio.services.sections.getCampuses = function (onSuccess, onError) {

    var url = "/api/admin/sections/campuses"
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
              
////GET ALL SECTIONS
sabio.services.sections.getSections = function (onSuccess, onError) {

    var url = "/api/admin/sections"
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

////GET ALL INSTRUCTORS
sabio.services.sections.getInstructors = function (onSuccess, onError) {

    var url = "/api/Instructors/List"
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


////GET ALL COURSES
sabio.services.sections.getCourses = function (onSuccess, onError) {

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

//REGISTER FOR A SECTION
sabio.services.sections.register = function (sectionId, onSuccess, onError) {

    var url = "/api/admin/sections/register/user";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: sectionId
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}

// update info/ description 
sabio.services.sections.updateInfo = function (id, data, onSuccess, onError) {
   
    var url = "/api/admin/sections/Info/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data:data
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

// Location Tab
sabio.services.sections.updateLocation = function (id, data, onSuccess, onError) {

    var url = "/api/admin/sections/Location/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , data: data
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

// section User By Id 
sabio.services.sections.getUsersInSection = function (id, onSuccess, onError) {

    var url = "/api/admin/sections/user/" + id;
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

sabio.services.sections.getAllStudentsByRole = function ( onSuccess, onError) {

    var url = "/api/user/students/";
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

// Add user to section 
sabio.services.sections.updateUser = function (id, sectionId, onSuccess, onError) {

    var url = "/api/admin/sections/" + id + "/User/" + sectionId;
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

//delete User from Section
sabio.services.sections.deleteUser = function (UserId,id, onSuccess, onError) {
    var url = "/api/admin/sections/" + UserId + "/User/" + id;
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

////GET ALL "CHOSEN" INSTRUCTORS
sabio.services.sections.getChosenInstructors = function (id, onSuccess, onError) {

    var url = "/api/admin/sections/Instructor/" + id;
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

sabio.services.sections.addSectionInstructors = function (instructorId,id, onSuccess, onError) {

    var url = "/api/admin/sections/" + instructorId + "/Instructor/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

sabio.services.sections.deleteSectionInstructor = function (instructorId, id, onSuccess, onError) {

    var url = "/api/admin/sections/" + instructorId + "/Instructor/" + id;
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

