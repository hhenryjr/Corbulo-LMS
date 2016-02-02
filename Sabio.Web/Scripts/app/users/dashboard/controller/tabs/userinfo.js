sabio.page.userDashboardInfoControllerFactory = function (
    $scope
    , $baseController
    , $dashboardService
    , $routeParams
    , $location
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$dashboardService = $dashboardService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;


    vm.receiveUserProfileItems = _receiveUserProfileItems;
    vm.onReceiveUserProfileError = _onReceiveUserProfileError;
    vm.receiveUserAddressSuccess = _receiveUserAddressSuccess;

    vm.notify = vm.$dashboardService.getNotifier($scope);
   
    _init();

    function _init() {
       
        vm.$dashboardService.getUserInfoByUserId(vm.receiveUserProfileItems, vm.onReceiveUserProfileError);
    }


    function _receiveUserProfileItems(data) {
      
        vm.notify(function () {       
            vm.user = data.item;      
        });
        var id = vm.user.addressId
        vm.$dashboardService.getAddressById(id, vm.receiveUserAddressSuccess, vm.receiveUserAddressError);      
    }

    function _receiveUserAddressSuccess(data) {
      
        vm.notify(function () {
            vm.userAddress = data.item;
        });
    }

    function _receiveUserAddressError(response) {
        console.log(response);
    }

    function _onReceiveUserProfileError(jqXhr, error) {
        console.log(error);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "userDashboardInfoController"
, ['$scope', '$baseController', '$dashboardService', '$routeParams', '$location']
, sabio.page.userDashboardInfoControllerFactory);