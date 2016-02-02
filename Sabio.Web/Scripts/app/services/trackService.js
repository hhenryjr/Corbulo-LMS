

sabio.services.tracks.factory = function ($baseService) {
    var aSabioServiceObject = sabio.services.tracks;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$trackService"
    , ["$baseService"]
    , sabio.services.tracks.factory);