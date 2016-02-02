sabio.page.handlers.onSubmitFormClicked = function (evt) {
    evt.preventDefault();
    //validate data

    //submit confirmation message
    //send home 
    

    var targetFormId = $(this).attr("data-formId");
    var targetForm = $(targetFormId);
    var targetCommentElement = null;

    if (sabio.page.lastReplyLink) {

        targetCommentElement = $(sabio.page.lastReplyLink).closest(".comment-content");
    };

    var formGrab = sabio.page.grabDataInput(targetForm);

    sabio.page.showComments();
    sabio.page.addComment(formGrab, targetCommentElement);

    targetForm[0].reset();
    sabio.page.lastReplyLink = null;
    $("#myModal").modal("hide");
};


sabio.objects = [];

sabio.page.wireUpReplies = function (context, data) {

    var repliesFound = $(".reply", context);
    //  console.log("Found these many replies to wire up:" + repliesFound.length);
    // console.dir(repliesFound);
    console.log(repliesFound);


    sabio.objects.push(repliesFound);

    repliesFound.on("click", sabio.page.handlers.onReplyClicked);


}



sabio.page.subtmitComments = function () {
    $(".eventForm").slideDown();
}


sabio.page.goTo = function (jQueryObject) {

    var topOfComments = jQueryObject.offset().top;

    var animateOptions = {
        scrollTop: topOfComments
    };

    $('html, body').animate(animateOptions, 2000);
}
