sabio.services.userDashboardFactory = function ($baseService) {
    var aSabioServiceObject = sabio.profiles.services.users;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}
sabio.ng.addService(sabio.ng.app.module
          , "$dashboardService"
          , ["$baseService"]
          , sabio.services.userDashboardFactory);