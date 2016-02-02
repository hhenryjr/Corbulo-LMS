//Seperation of concerns
if (!sabio.services.wikis) {
    sabio.services.wikis = {}
}

//ADD Ajax Call
sabio.services.wikis.add = function (wikiData, onSuccess, onError) {
    var url = "/api/Wiki";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: wikiData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

//PUT Ajax Call
sabio.services.wikis.update = function (id, wikiData, onSuccess, onError) {

    var url = "/api/Wiki/" + id;

    var settings = {
        cache: false
              , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
              , data: wikiData
              , dataType: "json"
              , success: onSuccess
              , error: onError
              , type: "PUT"
    };

    $.ajax(url, settings);

}

//GET By Id Ajax Call
sabio.services.wikis.get = function (id, onSuccess, onError) {

    var url = "/api/Wiki/" + id;
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

//GET All Call on List view
sabio.services.wikis.getAll = function (onSuccess, onError) {

    var url = "/api/Wiki/list";
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


sabio.services.wikis.getByType = function (pageData, typeId, onSuccess, onError) {

    var url = "/api/wiki/type/" + typeId;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: pageData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}


sabio.services.wikis.getAllSpaces = function (onSuccess, onError) {
    var url = "/api/wiki/Spaces";
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

//Delete Ajax call on List view
sabio.services.wikis.delete = function (id, onSuccess, onError) {

    var url = "/api/Wiki/Delete/" + id;
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

// update & delete based on CheckBox
sabio.services.wikis.updateSpaceId = function (id, spaceId, onSuccess, onError) {

    var url = "/api/wiki/" + id + "/spaces/" + spaceId;
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
//delete spaces by page id + space id (checkbox)
sabio.services.wikis.deleteSpace = function (id, spaceId, onSuccess, onError) {
    var url = "/api/wiki/" + id + "/spaces/" + spaceId;

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

//get by Space Id can also be found in wikiTree.js
sabio.services.wikis.getSpaceById = function (id, onSuccess, onError) {

    var url = "/api/Wiki/spaces/" + id + "/pages";
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

sabio.services.wikis.getTags = function (onSuccess, onError) {

    var url = "/api/tags";
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

sabio.services.wikis.addTag = function (id, tagId, onSuccess, onError) {

    var url = "/api/wiki/" + id + "/tags/" + tagId;
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

sabio.services.wikis.deleteTag = function (id, onSuccess, onError) {

    var url = "/api/wiki/tags/" + id;

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

sabio.services.wikis.getAllInstructors = function (onSuccess, onError) {
    var url = "/api/instructors/list";
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

sabio.services.wikis.addCourseInstructor = function (courseId, id, onSuccess, onError) {

    var url = "/api/admin/wiki/" + courseId + "/Instructor/" + id;
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

sabio.services.wikis.deleteCourseInstructor = function (courseId, id, onSuccess, onError) {

    var url = "/api/admin/wiki/" + courseId + "/Instructor/" + id;
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

