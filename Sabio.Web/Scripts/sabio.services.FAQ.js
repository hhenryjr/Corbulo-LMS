
if (!sabio.faq) {
    sabio.faq = { services: { FAQ: {} } };
}

sabio.faq.services.FAQ.get = function (onSuccess, onError) {

    var url = "/api/faq/list";
    var setting = {
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        dataType: "json",
        success: onSuccess,
        error: onError
    }
    $.ajax(url, setting);

}