if (!sabio.services.address) {
    sabio.services.address = {};   //if this namespace is not present than make it present 
}

//Call AJAX POST(forAPI/Service)                     //Inserts data in your database after client clicks submits button
sabio.services.address.add = function (addressData, onSuccess, onError) {

    var url = "/api/Addresses";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: addressData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings); //Ajax calls

}

//Call AJAX PUT UPDATE(forAPI/service)           // Updating data from client-side to database by using an id
sabio.services.address.update = function (id, myData, onSuccess, onError) {      //calls the update function to connect to the API controller;

    var url = "/api/Addresses/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"                          // gives a data type json >> give JavaScript object notation which is an array
        , success: onSuccess     //if it successful it will run this function
        , error: onError                // if not it will run this function
        , type: "PUT"                               //calling the PUT method which is in my API controller
    };

    $.ajax(url, settings);                          //calling the Ajax function (using the URL and settings) to the API controller
    //packaging data a sends it to the API controller

}

//Call AJAX GET(forAPI/service)  
sabio.services.address.get = function (id, onSuccess, onError) {        //calls the update function to connect to the API controller;
    console.log("GET Working");

    var url = "/api/Addresses/" + id;         // id you put on the URL on the browser

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"                          // gives a data type json >> give JavaScript object notation which is an array
        , success: onSuccess        //if it successful it will run this function
        , error: onError                // if not it will run this function
        , type: "GET"                               //calling the PUT method which is in my API controller
    };

    $.ajax(url, settings);                          //calling the Ajax function (using the URL and settings) to the API controller
    //packaging data a sends it to the API controller

}

sabio.services.address.getCountries = function (onSuccess, onError) {

    var url = "/api/Addresses/Countries/";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: myData
        , dataType: "json"
        , success: sabio.page.getCountriesSuccess
        , error: sabio.page.ajaxError
        , type: "GET"
    };

    $.ajax(url, settings);

}

sabio.services.address.onSelectChangeState = function (id, onSuccess, onError) {

    var url = "/api/Addresses/States/" + id;
    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: myData
    , dataType: "json"
    , success: sabio.page.getStateProvincesSuccess
    , error: sabio.page.ajaxError
    , type: "GET"
    };

    $.ajax(url, settings);
}

//Call AJAX DELETE(forAPI/service)
//sabio.page.deleteAjaxAddresses = function () {
//    console.log("delete");
//    var id = $("#addressId").val();
//    var url = "/api/Addresses/Delete/" + id;
//    var settings = {
//        cache: false
//        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
//        , dataType: "json"
//        , success: sabio.page.deleteAjaxSuccess
//        , error: sabio.page.ajaxError
//        , type: "Delete"
//    };

//    $.ajax(url, settings);
//}

//AJAX Call List View 
sabio.services.address.getAddresses = function (onSuccess, onError) {

    var url = "/api/Addresses";
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

//AJAX Call List Delete
sabio.services.address.delete = function (id, onSuccess, onError) {

    var url = "/api/Addresses/" + id;
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


