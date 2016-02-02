if (!sabio.services.testimonials) {

	sabio.services.testimonials = {};
}

sabio.services.testimonials.Get = function (id, onSuccess, onError) {

    var url = "/api/admin/testimonial/" + id;
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };
    $.ajax(url, setting);
};

sabio.services.testimonials.Post = function (myData, onSuccess, onError) {

    var url = "/api/admin/testimonial"
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "POST"
    };
    $.ajax(url, setting);
};

sabio.services.testimonials.Update = function (id, myData, onSuccess, onError) {

    var url = "/api/admin/testimonial/" + id;
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: myData,
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "PUT"
    };
    $.ajax(url, setting);
};

sabio.services.testimonials.Delete = function (id, onSuccess, onError) {

    var url = "/api/admin/testimonial/" + id;
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "DELETE"
    };
    $.ajax(url, setting);
};

sabio.services.testimonials.GetList = function (onSuccess, onError) {

    var url = "/api/admin/testimonial/list"
    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };
    $.ajax(url, setting);
};