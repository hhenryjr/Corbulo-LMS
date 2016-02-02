sabio.page.adminMeetingsIndexControllerFactory = function (
    $scope
    , $baseController
    , $meetingsService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$meetingsService = $meetingsService;
    vm.notify = vm.$meetingsService.getNotifier($scope);

    vm.meetings = null;

    _init();

    function _init() {

        vm.$meetingsService.getAll(_onGetSuccess, _onGetError);
    }

    function _onGetSuccess(response) {
        vm.notify(function () {
            vm.meetings = response.items;
        });

        console.log("Got all from server", vm.sections);
    }

    function _onGetError(response) {
        console.error("Getting all from server failed!", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
    , "adminMeetingsIndexController"
    , ['$scope', '$baseController', '$meetingsService']
    , sabio.page.adminMeetingsIndexControllerFactory);