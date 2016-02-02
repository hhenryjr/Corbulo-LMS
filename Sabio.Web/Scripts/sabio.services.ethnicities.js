if (!sabio.services.ethnicities) {

    sabio.services.ethnicities = {};
}

sabio.services.ethnicities.getAll = function (onSuccess, onErrorr) {
    var url = "/api/user/ethnicities";
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onErrorr,
        type: "GET"

    };
    $.ajax(url, setting);
}