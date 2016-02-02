//Seperation of concerns
if (!sabio.services.instructors) {
    sabio.services.instructors = {}
}

//AJAX Delete Call
sabio.services.instructors.delete = function (id, onSuccess, onError) {

    //var id = $("#id").val();
    var url = "/api/instructors/" + id;
    alert("Record #" + " " + id + " " + "has been deleted");
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


//Call AJAX POST(forAPI/Service)//POST!!!
sabio.services.instructors.addAjaxCall = function (data, onSuccess, onError) {

    var url = "/api/Instructors";
    //var myData = $('#bio-form').serialize();

    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , data: data
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "POST"
    };

    $.ajax(url, settings);

}

//Call AJAX PUT UPDATE(forAPI/service)
sabio.services.instructors.updateAjaxCall = function (id, myData, onSuccess, onError) {

    var url = "/api/Instructors/" + $("#bioId").val();
    var myData = $('#bio-form').serialize();
    var myData2 = "id=" + $("#bioId").val() + "&" + myData;
    console.log(myData2);
    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , data: myData
    , dataType: "json"
    , success:onSuccess
    , error: onError
    , type: "PUT"
    };

    $.ajax(url, settings);


}

//Call AJAX GET
sabio.services.instructors.getDataAjaxCall = function (id, onSuccess, onError) {

    var id = $("#bioId").val();
    var url = "/api/Instructors/" + id;
    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    ,data: id
    , dataType: "json"
    , success:onSuccess
    , error:onError
    , type: "GET"
    };

    $.ajax(url, settings);


}

//Call Ajax GET ALL
sabio.services.instructors.getAll = function (onSuccess, onError) {
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