sabio.services.modulesfactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.modules;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}
sabio.ng.addService(sabio.ng.app.module
    , "$modulesService"
    , ["$baseService"]
    , sabio.services.modulesfactory);