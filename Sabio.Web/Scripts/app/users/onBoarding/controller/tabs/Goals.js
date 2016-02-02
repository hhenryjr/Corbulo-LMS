sabio.page.onBoardGoalsControllerFactory = function (
    $scope
    , $baseController
    , $officeHoursService
    , $routeParams
    , $alertService
    , $utilityService
    , $uibModal
    ) {

    var vm = this;
    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$officeHoursService = $officeHoursService;
    vm.$routeParams = $routeParams;
    vm.$utilityService = $utilityService;

    vm.notify = vm.$officeHoursService.getNotifier($scope);

    //vm.timezones = null;


    //vm.OfficeHours = {};
    //vm.officeHourId = null;
    vm.settings = _settings;

    _init();

    function _init() {
        _settings();
        //vm.$officeHoursService.getByDate(_onSuccess, _onError);

        //var tz = JSON.parse($("#timezones").html());
        //vm.timezones = [];
        //angular.forEach(tz, function (val, key) {
        //    vm.timezones.push({
        //        id: key,
        //        value: val
        //    })
        //});
    }

    function _settings() {
      
    }
    //function _onSuccess(respone) {
    //    vm.notify(function () {
    //        var timeRelated = respone.items;
    //        for (var i = 0; i < timeRelated.length; i++) {
    //            _initOfficeHours(timeRelated[i]);
    //        }
    //        vm.OfficeHours = timeRelated;
    //    });
    //}

 
}

sabio.ng.addController(sabio.ng.app.module
, "userOnBoardingGoalsController"
, ['$scope', '$baseController', '$officeHoursService', '$routeParams', '$alertService', '$utilityService', '$uibModal']
, sabio.page.onBoardGoalsControllerFactory);