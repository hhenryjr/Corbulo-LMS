
sabio.page.netUserControllerFactory = function ($scope, $baseController, $rolesService, $routeParams) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;

    vm.$rolesService = $rolesService;

    vm.getUserSuccess = _getUserSuccess;
    vm.getUsersError = _getUsersError;

    vm.notify = vm.$rolesService.getNotifier($scope);

    render();

    function render() {
        vm.$rolesService.getNetUsers(vm.getUserSuccess, vm.getUsersError);
    }

    function _getUserSuccess(data) {
        vm.notify(function () {
            vm.users = data.items;
        }); 
    }
    function _getUsersError(jqXhr, error) {
        console.log(error);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "NetUserController"
, ['$scope', '$baseController', '$rolesService']
, sabio.page.netUserControllerFactory);