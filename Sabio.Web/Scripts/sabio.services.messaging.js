if (!sabio.services.messaging) {

    sabio.services.messaging = {};
}

sabio.services.messaging.contactMessage = function (contactData, onSuccess, onError) {

    var url = "/api/message/ContactUs";

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: contactData,
        success: onSuccess,
        error: onError,
        type: "post"

    };
    $.ajax(url, setting);

}


//neil adding to send confirmation email
sabio.services.messaging.sendConfirmEmail = function (contactData, onSuccess, onError) {

    var url = "/api/message/ConfirmEmail";

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: contactData,
        success: onSuccess,
        error: onError,
        type: "post"

    };
    $.ajax(url, setting);

}

//sending forgot password confirmation email
sabio.services.messaging.sendForgotPassEmail = function (contactData, onSuccess, onError) {

    var url = "/api/message/ForgotPass";

    var setting = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: contactData,
        success: onSuccess,
        error: onError,
        type: "post"

    };
    $.ajax(url, setting);

}
