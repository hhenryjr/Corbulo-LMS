//SEPARATION OF CONCERNS

if (!sabio.services.blogs) {
    sabio.services.blogs = {};
}

//Calls from Index View
//AJAX Call POST               C
sabio.services.blogs.add = function (blogData, onSuccess, onError) {

    var url = "/api/blogs/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: blogData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

}


//AJAX Call GET                R
sabio.services.blogs.get = function (id, onSuccess, onError) {

    //var id = $("#trackId").val();
    var url = "/api/blogs/" + id;
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


//AJAX Call PUT                U
sabio.services.blogs.update = function (id, myData2, onSuccess, onError) {

    var url = "/api/blogs/" + id;
    //var myData = $('#courseTrack-form').serialize();
    //var myData2 = "id=" + id + "&" + myData;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: myData2
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}


//AJAX Call DELETE             D
sabio.services.blogs.delete = function (id, onSuccess, onError) {
    //console.log alert("Record #" + " " + id + " " + "has been deleted");
    var url = "/api/blogs/" + id;

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

//Calls from List View
//AJAX Call //Get tracks
sabio.services.blogs.getBlogs = function (onSuccess, onError) {
    var url = "/api/blogs/";
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

sabio.services.blogs.getBlogsByTagName = function (tagName, onSuccess, onError) {
    var url = "/api/blogs/tags/" + tagName;
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

sabio.services.blogs.getTagsUsedByBlogs = function (onSuccess, onError) {

    var url = "/api/blogs/blogTags";
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