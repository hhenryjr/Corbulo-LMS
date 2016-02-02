sabio.page.adminAttendanceDayControllerFactory = function (
    $scope
    , $baseController
    , $attendanceService
    , $routeParams
    ) {

    var vm = this;

    vm.headerContent = {
        title: "Manage <strong>Attendance</strong>",
        subtitle: "Learning Command System"
    };

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$attendanceService = $attendanceService;
    vm.$routeParams = $routeParams;
    vm.notify = vm.$attendanceService.getNotifier($scope);

    vm.onCampusesGetSuccess = _onCampusesGetSuccess;
    vm.onCampusesGetError = _onCampusesGetError;

   // vm.submitBasic = _submitBasic;
    vm.datepickerDisabled = _datepickerDisabled;
    vm.openDatepicker = _openDatepicker;
    vm.showSectionFormErrors = false;
    vm.datepicker = {
        format: 'dd-MMMM-yyyy'
        , status: {
            checkIn: false           
        }
        , options: {
            formatYear: 'yy',
            startingDay: 1
        }
        , defaults: {
            checkIn: { min: new Date(), max: new Date(2020, 5, 22) }           
        }
    };

    vm.campuses = [];

    //vm.update = _update;

    vm.attendance = null;

    _init();

    function _init() {

        vm.$attendanceService.getAttendance(_onGetAttendanceSuccess, _onGetAttendanceError);
        vm.$attendanceService.getCampuses(_onCampusesGetSuccess, _onCampusesGetError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }

    function _openDatepicker($event, type) {
        angular.forEach(vm.datepicker.status, function (val, key) {
            vm.datepicker.status[key] = false;
        });

        vm.datepicker.status[type] = true;
    }

    function _datepickerDisabled(date, mode) {
        return false; //( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
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

    function _onCampusesGetSuccess(response) {
        vm.notify(function () {
            vm.campuses = response.items;
        });
    }

    function _onCampusesGetError(response) {
        console.error("error getting campuses", response);
    }
   
}

sabio.ng.addController(sabio.ng.app.module
, "adminAttendanceDayController"
, ['$scope', '$baseController', '$attendanceService','$routeParams']
, sabio.page.adminAttendanceDayControllerFactory);