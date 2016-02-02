if (!sabio.services.payments) {
    sabio.services.payments = {};
}

sabio.services.payments.Add = function(myData,onSuccess,onError)
{
    var url = "/api/admin/payment";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}