sabio.services.attendance.factory = function ($baseService) {
    var aSabioServiceObject = sabio.services.attendance;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
    , "$attendanceService"
    , ["$baseService"]
    , sabio.services.attendance.factory);