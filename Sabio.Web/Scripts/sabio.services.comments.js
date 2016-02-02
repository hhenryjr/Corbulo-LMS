if (!sabio.services.comments) {
    sabio.services.comments = {};
}
//get 
sabio.services.comments.getComment = function (id, onSuccess, onErrorr) {

    var url = "/api/comments/" + id ;
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

sabio.services.comments.getCommentsByOwnertype = function (ownerId, owerType, onSuccess, onErrorr) {

    var url = "/api/comments/" + ownerId + "/" + owerType;
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

//get
sabio.services.comments.getCommentsByOwnerTypeAndParentId = function (ownerId, ownerTypeId, parentId, onSuccess, onErrorr) {

    var url = "/api/comments/" + ownerId + "/" + ownerTypeId + "/" + parentId;
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

//Add
sabio.services.comments.addComment = function (commentData, onSuccess, onError) {

    var url = "/api/Comments/";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: commentData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "Post"
    };
    $.ajax(url, settings);
}

//delete
sabio.services.comments.deleteComment= function (id, onSuccess, onError) {
    var url = "/admin/users/" + id;

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

//put

sabio.services.comments.EditComment = function (id, commentData, onSuccess, onError) {

    var url = "/api/Comments/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: commentData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}