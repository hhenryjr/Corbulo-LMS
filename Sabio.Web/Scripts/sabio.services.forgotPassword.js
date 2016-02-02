

if (!sabio.services.forgotPassword) {
	sabio.services.forgotPassword = {};    //create an object 
}

// http:Post
sabio.services.forgotPassword.sendForgotPasswordRequest = function (emailData, onSuccess, onError) {
    
    var url = "/API/User/SendEmail";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: emailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}