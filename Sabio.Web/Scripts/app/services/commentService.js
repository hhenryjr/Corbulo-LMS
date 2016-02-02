sabio.services.commentsFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.comments;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$commentService"
    , ["$baseService"]
    , sabio.services.commentsFactory);