
sabio.services.dashboardSidebarFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.dashboardSidebar;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.services.sidebarControllerFactory = function ($scope, $baseController, $sidebarServices) {
    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$sidebarServices = $sidebarServices;
    vm.$scope = $scope;

    vm.receiveItems = _receiveItems;
    vm.onReceiveSpaces = _onReceiveSpaces;
    vm.onReceiveError = _onReceiveError;
    vm.onSpaceError = _onSpaceError;

    render();

    function render() {

        console.log("sidebar services engaging!");
        vm.$sidebarServices.getAll(vm.receiveItems, vm.onReceiveError);
        vm.$sidebarServices.getAllSpaces(vm.onReceiveSpaces, vm.onSpaceError);
    }

    function _receiveItems(data) {
        vm.notify(function () {
            vm.items = data.items;
            console.log(vm.items)
        });

        function _onReceiveSpaces(data) {
            vm.notify(function () {
                vm.items = data.items;
            });
        }

        function _onReceiveError(jqXhr, error) {
            console.log(error);
        }

        function _onSpaceError(jqXhr, error) {
            console.log(error);
        }
    }
}

sabio.ng.addService(sabio.ng.app.module
    , "$sidebarServices"
    , ["$baseService"]
    , sabio.services.dashboardSidebarFactory);

sabio.ng.addController(sabio.ng.app.module
    , "$sideBarController"
    , ['$scope', '$baseController', '$sidebarServices']
    , sabio.services.sidebarControllerFactory);
