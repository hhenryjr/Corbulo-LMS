sabio.page.officeHoursControllerFactory = function (
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

    vm.timezones = null;
    

    vm.OfficeHours = {};
    vm.officeHourId = null;
    vm.settings = _settings;

    _init();

    function _init() {
        _settings();
        vm.$officeHoursService.getByDate(_onSuccess, _onError);

        var tz = JSON.parse($("#timezones").html());
        vm.timezones = [];
        angular.forEach(tz, function (val, key) {
            vm.timezones.push({
                id: key,
                value: val
            })
        });
    }

    function _settings() {
        vm.accordionOpen = true;
        vm.accordionDisabled = true;
    }
    function _onSuccess(respone) {
        vm.notify(function () {
            var timeRelated = respone.items;
            for (var i = 0; i < timeRelated.length; i++) {
                _initOfficeHours(timeRelated[i]);
            }
            vm.OfficeHours = timeRelated;
        });
    }

    function _initOfficeHours(timeRelated) {

        timeRelated.startTime = vm.$utilityService.dateFromMilitaryTime(timeRelated.startTime);
        timeRelated.endTime = vm.$utilityService.dateFromMilitaryTime(timeRelated.endTime);

        for (x = 0; x <= vm.timezones.length; x++) {
            if (timeRelated.timeZone == vm.timezones[x].id) {
                timeRelated.timeZone = vm.timezones[x];
                break;
            }
        }
       
    }
    function _onError(response) {
        vm.$alertService.onError("There was a problem loading officeHours please contact Tech Support")
    }
    vm.open = function (id) {
       
        vm.items = [];
        vm.items.push(id);
        var questionModal = {
            animation: vm.animationsEnabled,
            templateUrl: 'modal-ask-question.html',
            controller: '$modalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return vm.items;
                }
            }
        };
        var contactModalInstance = $uibModal.open(questionModal);
    }


}

sabio.ng.addController(sabio.ng.app.module
, "userOffficeHoursController"
, ['$scope', '$baseController', '$officeHoursService', '$routeParams', '$alertService', '$utilityService', '$uibModal']
, sabio.page.officeHoursControllerFactory);

sabio.services.modalFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.officeHours;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.page.modalControllerFactory = function (
    $scope,
    $modalInstance,
    $modalService,
    $location,
    $route,
    $templateCache,
    $routeParams,
    items
    ) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $modalInstance;
    vm.$modalService = $modalService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.items = items;

    vm.qa = {};
    vm.addQuestion = _addQuestion;
    vm.onSuccess = _onSuccess;
    vm.onError = _onError;
    vm.reloadTemplate = _reloadTemplate;

    _init();

    function _init() {
        vm.showErrors = false;
    }
    vm.cancel = function () {
        $modalInstance.close();
    }

    function _addQuestion(cmt, id) {
        _validateForm(cmt, id);
       
    }
    function _validateForm(cmt, id) {
        vm.showErrors = true;
        if (vm.reviewForm.$valid) {   
            vm.$modalService.AddQuestion(id, cmt, _onSuccess, _onError);
            $modalInstance.close();
           
        }
    }

    function _onSuccess(response) {
        console.log(response); 

    }
    function _onError(response) {
        console.log(response);
    }

    function _reloadTemplate() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }

}

sabio.ng.addService(sabio.ng.app.module
            , "$modalService"
            , ["$baseService"]
            , sabio.services.modalFactory);


sabio.ng.addController(sabio.ng.app.module
            , "$modalController"
            , ['$scope', '$modalInstance', '$modalService', '$routeParams', '$location', '$route', '$templateCache', 'items']
            , sabio.page.modalControllerFactory);