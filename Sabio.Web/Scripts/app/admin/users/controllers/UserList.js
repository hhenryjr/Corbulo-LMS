sabio.page.adminUserListControllerFactory = function (
    $scope
    , $baseController
    , $userSectionsService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$userSectionsService = $userSectionsService;

    vm.notify = vm.$userSectionsService.getNotifier($scope);

    vm.onGetUserListSuccess = _onGetUserListSuccess;
    vm.onGetUserListError = _onGetUserListError;
   
    render();

    function render() {
        vm.$userSectionsService.getUserList(vm.onGetUserListSuccess, vm.onGetUserListError);
    }

    function _onGetUserListSuccess(data) {
        vm.notify(function () {
            vm.userList = data.items;
        });
    }

    function _onGetUserListError(response) {
        console.log(response);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminUserListController"
, ['$scope', '$baseController', '$userSectionsService']
, sabio.page.adminUserListControllerFactory);