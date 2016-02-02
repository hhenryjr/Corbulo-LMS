sabio.services.alert = {
    factory: function ($baseService) {
        var svc = this;

        svc = $baseService.merge(true, {}, svc, $baseService);

        svc.onSuccess = _onSuccess;
        svc.onError = _onError;
        svc.onWarning = _onWarning;

        _init();

        function _init()
        {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "1000",
                "hideDuration": "1000",
                "timeOut": "3000",
                "extendedTimeOut": "2500",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
        }

        function _onSuccess(data) {            
            toastr.success(data);
        }

        function _onError(data) {
            toastr.error(data);
        }

        function _onWarning(data) {
            toastr.warning(data);
        }

        return svc;
    }
}

sabio.ng.addService(sabio.ng.app.module
    , "$alertService"
    , ["$baseService"]
    , sabio.services.alert.factory);