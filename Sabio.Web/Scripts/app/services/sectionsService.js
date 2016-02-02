sabio.services.sections.factory = function ($baseService) {
    var aSabioServiceObject = sabio.services.sections;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$sectionsService"
    , ["$baseService"]
    , sabio.services.sections.factory);

