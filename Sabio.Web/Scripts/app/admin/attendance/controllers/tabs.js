sabio.page.adminAttendanceTabsControllerFactory = function (
    $baseController
    , $scope
    , $routeParams
    , $location
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    // vm.$attendanceService = $attendanceService;
    //vm.selectTab = _selectTab;
    vm.tabs = null;

    // vm.notify = vm.$attendanceService.getNotifier($scope);

    vm.tabs = [
        { label: 'Day', link: '#/day', tab: 'day' },
        { label: 'User', link: '#/user', tab: 'user' },
        { label: 'Nearby Students', link: '#/nearbyStudents', tab: 'nearbyStudents' }
    ];

    vm.selectedTab = vm.tabs[0];
    vm.setSelectedTab = function (tab) {
        vm.selectedTab = tab;
    }

    vm.tabClass = function (tab) {
        if (vm.selectedTab.label === tab.label) {
            return "active";
        } else {
            return "";
        }
    }


}

sabio.ng.addController(sabio.ng.app.module
, "adminAttendanceTabsController"
, ['$baseController', '$scope', '$routeParams', '$location' /*,' $attendanceService'*/]
, sabio.page.adminAttendanceTabsControllerFactory);