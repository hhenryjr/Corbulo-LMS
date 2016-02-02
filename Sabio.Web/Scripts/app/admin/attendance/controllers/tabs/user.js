sabio.page.adminAttendanceUserControllerFactory = function (
    $scope
    , $baseController
    , $attendanceService
    , $routeParams
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$attendanceService = $attendanceService;
    vm.notify = vm.$attendanceService.getNotifier($scope);

    vm.attendance = null;

    vm.headerContent = {
        title: "Manage <strong>Attendance</strong>",
        subtitle: "Learning Command System"
    };

    _init();

    function _init() {

        vm.$attendanceService.getAllCheckedIn(_onGetAttendanceSuccess, _onGetAttendanceError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }

    function _onGetAttendanceSuccess(response) {
        vm.notify(function () {
            vm.attendance = response.items;
        });

        console.log("attendance from server", vm.attendance);
    }

    function _onGetAttendanceError(response) {
        console.error("error getting attendance", response);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminAttendanceUserController"
, ['$scope', '$baseController', '$attendanceService','$routeParams', '$location']
, sabio.page.adminAttendanceUserControllerFactory);