
sabio.services.userSectionsfactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.userSections;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}
sabio.ng.addService(sabio.ng.app.module
    , "$userSectionsService"
    , ["$baseService"]
    , sabio.services.userSectionsfactory);