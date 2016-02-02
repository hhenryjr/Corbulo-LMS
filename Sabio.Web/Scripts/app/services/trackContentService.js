sabio.services.trackContent.factory = function ($baseService) {
    var aSabioServiceObject = sabio.services.trackContent;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$trackContentService"
    , ["$baseService"]
    , sabio.services.trackContent.factory);