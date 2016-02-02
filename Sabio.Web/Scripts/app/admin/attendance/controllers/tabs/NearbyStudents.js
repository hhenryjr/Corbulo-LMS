sabio.page.adminNearbyStudentsControllerFactory = function (
    $scope
    , $baseController
    , $attendanceService
    , $sectionsService
    , $routeParams
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$attendanceService = $attendanceService;
    vm.$sectionsService = $sectionsService;
    vm.getNearby = _getNearby;
    vm.onGetNearbyAttendanceSuccess = _onGetNearbyAttendanceSuccess;
    vm.onGetNearbyAttendanceError = _onGetNearbyAttendanceError;
    vm.notify = vm.$attendanceService.getNotifier($scope);
    vm.onCampusesGetSuccess = _onCampusesGetSuccess;
    vm.onCampusesGetError = _onCampusesGetError;

    vm.attendance = null;

    vm.headerContent = {
        title: "Nearby <strong>Students</strong>",
        subtitle: "Learning Command System"
    };

    _init();

    function _init() {
        settings();
        vm.$sectionsService.getCampuses(_onCampusesGetSuccess, _onCampusesGetError)
        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }

    function _getNearby(id) {
        vm.$attendanceService.getNearbyAttendance(id, vm.onGetNearbyAttendanceSuccess, vm.onGetNearbyAttendanceError);
    }



    function _onGetNearbyAttendanceSuccess(response) {

        if (response.items == null) {
            settings();
        } else {
            vm.list = true;
            vm.notify(function () {
                vm.attendance = response.items;
            });
        }
    }

    function _onCampusesGetSuccess(response){
        vm.notify(function () {
            vm.campus = response.items;
        });
    }

    function _onCampusesGetError(response){
        console.log(response);
    }

    function _onGetNearbyAttendanceError(response) {
        console.error("error getting attendance", response);
    }

    function settings() {
        vm.list = false;
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminNearbyStudentsController"
, ['$scope', '$baseController', '$attendanceService', '$sectionsService', '$routeParams', '$location']
, sabio.page.adminNearbyStudentsControllerFactory);