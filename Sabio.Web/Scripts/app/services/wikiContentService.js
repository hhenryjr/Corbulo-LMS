    sabio.services.wikiContent.factory = function ($baseService) {
        var aSabioServiceObject = sabio.services.wikiContent;
        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
        return newService;
    }

    sabio.ng.addService(sabio.ng.app.module
        , "$wikiContentService"
        , ["$baseService"]
        , sabio.services.wikiContent.factory);   