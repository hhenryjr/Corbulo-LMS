    sabio.services.wikis.factory = function ($baseService) {
        var aSabioServiceObject = sabio.services.wikiTree;
        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
        return newService;
    }

    sabio.ng.addService(sabio.ng.app.module
        , "$wikiTreeService"
        , ["$baseService"]
        , sabio.services.wikis.factory);   