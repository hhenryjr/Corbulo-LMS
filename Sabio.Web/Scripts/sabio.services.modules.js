//SEPARATION OF CONCERNS

if (!sabio.services.modules) {
    sabio.services.modules = {};
}

sabio.services.modules.getSectionModules = function (sectionId, onSuccess, onError) {

    var url = "/api/admin/modules/section/" + sectionId;
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"

    };
    $.ajax(url, setting);
}

//AJAX Call POST                
sabio.services.modules.addModule = function (modulesData, onSuccess, onError)
{
    var url = "/api/admin/modules";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: modulesData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}

sabio.services.modules.addSectionModule = function (id, sectionId, onSuccess, onError)
{
    var url = "/api/admin/modules/sectionmodules/" + sectionId

    var modulesData = "moduleId=" + id + "&sectionId=" + sectionId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: modulesData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"

    };
    $.ajax(url, settings);
}

//AJAX Call GET                 R
sabio.services.modules.getModuleDetail = function (id, onSuccess, onError)
{ 
    var url = "/api/Modules/" + id;
  
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

//AJAX Call PUT                 U
sabio.services.modules.updateModule = function (id, moduleData, onSuccess, onError)
{
    var url = "/admin/Modules/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: moduleData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}

//AJAX Call Delete              D
sabio.services.modules.deleteModule = function (id, onSuccess, onError) {

    var url = "/admin/Modules/" + id;

    var setting = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };
    $.ajax(url, setting);

}

//AJAX Call Get List
sabio.services.modules.getList = function (onSuccess, onError) {
   
        var url = "/api/admin/modules";
        var setting = {
            cache: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "json",
            success: onSuccess,
            error: onError,
            type: "GET"

        };
        $.ajax(url, setting);
}

//gets all the wiki pages     
sabio.services.modules.getAllPages = function (onSuccess, onError) {

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

sabio.services.modules.getModuleTree = function (moduleId, onSuccess, onError) {

    var url = "/api/admin/modules/" + moduleId + "/tree";
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

//add a wikipage to a module
sabio.services.modules.addWikiPage = function (moduleId, wikipageId, onSuccess, onError) {

    var url = "/api/admin/modules/" + moduleId + "/page";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
        , data: { wikiPageId: wikipageId }
    };
    $.ajax(url, settings);
}

//delete a wikipage to a module
sabio.services.modules.deleteWikiPage = function (id, wikipageId, onSuccess, onError) {

    var url = "/admin/modules/" + id + "/wiki/" + wikipageId;

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

//gets all the wiki pages associated with the module
sabio.services.modules.getModuleWikis = function (id, onSuccess, onError) {

    var url = "/admin/Modules/wiki/" + id;

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