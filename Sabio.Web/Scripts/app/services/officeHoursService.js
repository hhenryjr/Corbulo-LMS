sabio.services.officeHourFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.officeHours;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.ng.addService(sabio.ng.app.module
        , "$officeHoursService"
        , ["$baseService"]
        , sabio.services.officeHourFactory);

