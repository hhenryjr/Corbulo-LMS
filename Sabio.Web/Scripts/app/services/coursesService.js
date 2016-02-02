sabio.services.coursesFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.courses;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
        , "$coursesService"
        , ["$baseService"]
        , sabio.services.coursesFactory);
