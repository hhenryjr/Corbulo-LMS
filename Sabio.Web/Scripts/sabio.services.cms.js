// check if there is an object

if (!sabio.services.cms) {
    sabio.services.cms = {};
}

// http:Post
sabio.services.cms.add = function (CMSdata, onSuccess, onError) {
    var url = "/api/CMS/validate";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: CMSdata
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}

// http:Update
sabio.services.cms.update = function (id, CMSdata, onSuccess, onError) {
    var url = "/api/CMS/validate/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: CMSdata
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    //console.log(settings);
    $.ajax(url, settings);
}

// http:Get
sabio.services.cms.getItem = function (id, onSuccess, onError) {
    var url = "/api/CMS/validate/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    //console.log(settings);
    $.ajax(url, settings);
}

// http:Get List
sabio.services.cms.getListItems = function (onSuccess, onError) {
    var url = "/API/CMS/List";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}

// http:Delete
sabio.services.cms.deleteItem = function (id, onSuccess, onError) {
    var url = "/api/CMS/Delete/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "Delete"
    };
    //console.log(settings);
    $.ajax(url, settings);
}