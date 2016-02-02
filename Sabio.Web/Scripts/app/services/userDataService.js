sabio.services.userDataFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.users;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}
sabio.ng.addService(sabio.ng.app.module
          , "$userDataService"
          , ["$baseService"]
          , sabio.services.userDataFactory);