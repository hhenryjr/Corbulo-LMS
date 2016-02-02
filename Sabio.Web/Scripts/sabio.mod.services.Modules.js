
if (!sabio.mod) {
    sabio.mod = { services: { modules: {} } };
}

sabio.mod.services.modules.getById = function (id, onSuccess, onError) {
    
    var url = "/api/Modules/"+ id;
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