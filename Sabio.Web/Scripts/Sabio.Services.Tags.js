//Seperation of concerns
if (!sabio.services.tags) {
    sabio.services.tags = {}
}

//Calls from Index View
//AJAX Post Call
sabio.services.tags.add = function (tagData, onSuccess, onError, domElm){

    var url = "/api/tags";
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: tagData
        , dataType: "json"
        , success: function(data)
        {
            onSuccess(data, domElm);

        }
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}


//AJAX Update Calls
sabio.services.tags.update = function (id, tagData, onSuccess, onError) {

    var url = "/api/tags/" + id ;
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: tagData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);

}

sabio.services.tags.updateActive = function (id, tagData, onSuccess, onError) {

    var url = "/api/tags/" + id + "/active";
    
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: tagData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);

}


//AJAX Get Call Not Used
sabio.services.tags.get = function (id, onSuccess, onError) {

    var url = "/api/tags/" + id;

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


//AJAX Delete Call Not used
sabio.services.tags.delete = function (id, onSuccess, onError) {

    ////var id = $("#id").val();
    //var url = "/api/tags/" + id;
    //alert("Record #" + " " + id + " " + "has been deleted");
    //var setting = {
    //    cache: false
    //    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    //    , dataType: "json"
    //    , success: onSuccess
    //    , error: onError
    //    , type: "DELETE"
    //};
    //$.ajax(url, setting);

}

//AJAX calls from list view
//AJAX Get all Call
sabio.services.tags.getTags = function (onSuccess, onError) {

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

