sabio.services.rolesfactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.roles;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$rolesService"
    , ["$baseService"]
    , sabio.services.rolesfactory);