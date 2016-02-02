sabio.services.meetings.factory = function ($baseService) {
    var aSabioServiceObject = sabio.services.meetings;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$meetingsService"
    , ["$baseService"]
    , sabio.services.meetings.factory);