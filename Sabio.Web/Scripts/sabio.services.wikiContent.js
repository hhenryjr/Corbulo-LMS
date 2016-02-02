//Seperation of concerns
if (!sabio.services.wikiContent) {
    sabio.services.wikiContent = {}
}

sabio.services.wikiContent.order = function (request, onSuccess, onError) {
    var url = "/api/admin/wiki/content/order";
    var settings = {
        cache: false
        , data: JSON.stringify({ content: request })
        , contentType: "application/json"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}


//ADD Ajax Call
sabio.services.wikiContent.add = function (request, onSuccess, onError) {
    var url = "/api/admin/wiki/content";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: request
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

sabio.services.wikiContent.getPageBySlug = function (pageSlug, onSuccess, onError) {

    var url = "/api/wiki/slug/" + pageSlug;
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

sabio.services.wikiContent.getByPageId = function (pageId, onSuccess, onError) {

    var url = "/api/admin/wiki/page/" + pageId + "/content";
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

sabio.services.wikiContent.update = function (contentId, data, onSuccess, onError) {

    var url = "/api/admin/wiki/content/" + contentId;
    var settings = {
        cache: false
        , data: JSON.stringify(data)
        , contentType: "application/json"        
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

sabio.services.wikiContent.deleteContent = function (contentId, onSuccess, onError) {

    var url = "/api/admin/wiki/content/" + contentId;
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