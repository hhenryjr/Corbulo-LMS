sabio.services.events = {
    factory: function ($baseService, $rootScope) {
        var svc = this;

        svc = $baseService.merge(true, {}, svc, $baseService);

        svc.$rootScope = $rootScope;

        svc.broadcast = _broadcast;
        svc.emit = _emit;
        svc.listen = _listen;

        function _broadcast(eventName) {
            svc.$rootScope.$broadcast(eventName, arguments)
        }

        function _emit(eventName) {
            svc.$rootScope.$emit(eventName, arguments)
        }

        function _listen(eventName, callback) {
            svc.$rootScope.$on(eventName, callback)
        }

        return svc;
    }
}

sabio.ng.addService(sabio.ng.app.module
    , "$eventHandlerService"
    , ["$baseService", "$rootScope"]
    , sabio.services.events.factory);