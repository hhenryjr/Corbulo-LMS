sabio.page.adminAttendanceDetailsControllerFactory = function (
    $scope
    , $baseController
    , $attendanceService
    , $routeParams
    , $route
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$attendanceService = $attendanceService;
    vm.notify = vm.$attendanceService.getNotifier($scope);
    vm.$route = $route;

    vm.onGetAttendanceByUserSuccess = _onGetAttendanceByUserSuccess;
    vm.onGetAttendanceByUserError = _onGetAttendanceByUserError;

    vm.attendance = null;

    vm.headerContent = {
        title: "Manage <strong>Attendance</strong>",
        subtitle: "Learning Command System"
    };

    _init();

    function _init() {

        vm.$attendanceService.getAttendanceByUser(vm.$routeParams.attendanceId, _onGetAttendanceByUserSuccess, _onGetAttendanceByUserError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }

    function _onGetAttendanceByUserSuccess(response) {
        vm.notify(function () {
            vm.attendance = response.items;
        });

        console.log("attendance from server", vm.attendance, vm.$routeParams.attendanceId);
    }

    function _onGetAttendanceByUserError(response) {
        console.error("error getting attendance", response);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminAttendanceDetailsController"
, ['$scope', '$baseController', '$attendanceService', '$routeParams', '$route']
, sabio.page.adminAttendanceDetailsControllerFactory);