
    sabio.services.wikis.factory = function ($baseService) {
        var aSabioServiceObject = sabio.services.wikis;
        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
        return newService;
    }

    sabio.ng.addService(sabio.ng.app.module
        , "$wikiService"
        , ["$baseService"]
        , sabio.services.wikis.factory);   