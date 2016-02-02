
if (!sabio.services.wikiTree) {
    sabio.services.wikiTree = { services: { Wiki: {} } };
}
// Get
//caling it from sabio.services.wiki.js
sabio.services.wikiTree.getAllPages = function (onSuccess, onError) {
    
    var url = "/api/Wiki/pages";
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

// gets space for dropdown
sabio.services.wikiTree.getAllSpaces = function (onSuccess, onError) {

    var url = "/api/Wiki/spaces";
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

// get space by Id

sabio.services.wikiTree.getSpaceById = function(id, onSuccess, onError) {

    var url = "/api/Wiki/spaces/"+id+"/pages" ;
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

// put for parentId
sabio.services.wikiTree.updateParentId = function(id, parentid, onSuccess, onError) {

    var url = "/api/wiki/"+id+"/Parent/" + parentid;

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

sabio.services.wikiTree.openPageOnSelect = function (id, onSuccess, onError) {

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
