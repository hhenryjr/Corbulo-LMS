sabio.page.officeHoursDetailsControllerFactory = function (
    $scope
    , $baseController
    , $officeHoursService
    , $routeParams
    , $location
    , $route
    , $templateCache
    , $utilityService
    , $uibModal
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$officeHoursService = $officeHoursService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.$utilityService = $utilityService;

    vm.openConfirmationModal = _openConfirmationModal;
    vm.getQuestionDeletion = _getQuestionDeletion;
    vm.getQuestionForDeletion = _getQuestionForDeletionError;
    vm.reload = _reload;
    vm.openModal = _openModal;
    vm.deleteQuestion = _deleteQuestion;
    vm.getQuestionSuccess = _getQuestionSuccess;
    vm.addResponse = _addResponse;
    vm.settings = _settings;
    vm.onGetOfficeHourDetailsError = _onGetOfficeHourDetailsError;
    vm.onGetOfficeHourDetailsSuccess = _onGetOfficeHourDetailsSuccess;
    vm.initSessionData = _initSessionData;

    vm.headerContent = {
        title: "<strong>Office Hour Session Details and Question Management</strong>",
        subtitle: "Sabio Learning Management System"
    };

    vm.notify = vm.$officeHoursService.getNotifier($scope);

    render();

    function render() {
        vm.settings();

        var tz = JSON.parse($("#timezones").html());
        vm.timezones = [];
        angular.forEach(tz, function (val, key) {
            vm.timezones.push({
                id: key,
                value: val
            })
        });

        vm.$officeHoursService.getOfficeHourSession(vm.$routeParams.officeHoursId, vm.onGetOfficeHourDetailsSuccess, vm.onGetOfficeHourDetailsError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }

    function _addResponse(id, oId) {
        console.log(id, oId);
        vm.$officeHoursService.getByOfficeHourIdAndQuestionId(oId, id, vm.getQuestionSuccess, vm.getQuestionError);
    }

    function _getQuestionSuccess(data) {
        _openModal(data);
    }

    function _getQuestionError(response) {
        console.log(response);
    }

    function _deleteQuestion(id, oId) {
        vm.$officeHoursService.getByOfficeHourIdAndQuestionId(oId, id, vm.getQuestionDeletion, vm.getQuestionForDeletionError);
    }

    function _openModal(qD) {
        vm.items = [];
        vm.items.push(qD.item);

        var responseModal = {
            animation: $scope.animationsEnabled,
            templateUrl: '/Scripts/app/admin/officeHours/templates/responseModal.html',
            controller: '$responseModalController',
            controllerAs: 'officeHour',
            resolve: {
                items: function () {
                    return vm.items;
                }
            }
        };
        var responseModalInstance = $uibModal.open(responseModal);
    }

    function _openConfirmationModal(qD) {
        vm.items = [];
        vm.items.push(qD.item);

        var confirmationModal = {
            animation: $scope.animationsEnabled,
            templateUrl: '/Scripts/app/admin/officeHours/templates/confirmQuestionDelete.html',
            controller: '$confirmationModalController',
            controllerAs: 'officeHour',
            resolve: {
                items: function () {
                    return vm.items;
                }
            }
        };
        var confirmationModalInstance = $uibModal.open(confirmationModal);
    }

    function _getQuestionDeletion(data) {
        _openConfirmationModal(data);
    }

    function _getQuestionForDeletionError(response) {
        console.log(response);
    }

    function _onGetOfficeHourDetailsSuccess(data) {
        var details = _initSessionData(data.item);
        vm.notify(function () {
            vm.details = details;
        });
    }

    function _initSessionData(details) {

        for (x = 0; x <= vm.timezones.length; x++) {
            if (details.timeZone == vm.timezones[x].id) {
                details.timeZone = vm.timezones[x];
                break;
            }
        }

        details.startTime = vm.$utilityService.dateFromMilitaryTime(details.startTime);
        details.endTime = vm.$utilityService.dateFromMilitaryTime(details.endTime);
        return details;

    }

    function _onGetOfficeHourDetailsError(jqXhr, error) {
        console.log(error);
    }

    function _settings() {
        vm.accordionOpen = true;
        vm.accordionDisabled = true;
    }

    function _reload() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }
}

///////////////////////////////////////////////

//RESPONSE MODAL

///////////////////////////////////////////////
sabio.page.responseModalFactory = function ($scope,
    $responseModalInstance
    ,items
    ,$officeHoursService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {
    var vm = this;
    vm.$scope = $scope;
    vm.$responseModalInstance = $responseModalInstance;
    vm.items = items;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.$officeHoursService = $officeHoursService;
    vm.addResponseError = _addResponseError;
    vm.addResponseSuccess = _addResponseSuccess;
    vm.validateResponseForm = _validateResponseForm;
    vm.reload = _reload;

    render();

    function render() {
        settings();
    }

    vm.ok = function (oId, Id) {
        vm.validateResponseForm(oId, Id);
    };

    vm.cancel = function () {
        $responseModalInstance.dismiss('cancel');
    };

    function _validateResponseForm(oId, Id) {
        vm.showResponseModalErrors = true;

        if (vm.officeHourForm.$valid) {
            var response = { response: vm.items[0].response }
            vm.$officeHoursService.addResponse(oId, Id, response, vm.addResponseSuccess, vm.addResponseError);
            $responseModalInstance.close();
        }
    }

    function _addResponseSuccess(response) {
        console.log(response);
        vm.reload();
    }

    function _addResponseError(response) {
        console.log(response);
    }

    function _reload() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }

    function settings() {
        vm.showResponseModalErrors = false;
    }
}


///////////////////////////////////////////////

//DELETE CONFIRMATION MODAL

///////////////////////////////////////////////
sabio.page.confirmationModalFactory = function ($scope,
    $confirmationModalInstance,
    items,
    $officeHoursService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {

    var vm = this;
    vm.$scope = $scope;
    vm.$confirmationModalInstance = $confirmationModalInstance;
    vm.items = items;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.$officeHoursService = $officeHoursService;

    vm.reload = _reload;
    vm.deleteQuestionSuccess = _deleteQuestionSuccess;
    vm.deleteQuestionError = _deleteQuestionError;

    vm.ok = function (Id) {
        vm.$officeHoursService.deleteQuestion(Id, vm.deleteQuestionSuccess, vm.deleteQuestionError);
        vm.reload();
        $confirmationModalInstance.close();
    };

    vm.cancel = function () {
        $confirmationModalInstance.dismiss('cancel');
    };

    function _deleteQuestionSuccess(response) {
        console.log(response);
        vm.reload();
    }

    function _deleteQuestionError(response) {
        console.log(response);
    }

    function _reload() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }
}

sabio.ng.addController(sabio.ng.app.module
, "$confirmationModalController"
, ['$scope', '$modalInstance', 'items', '$officeHoursService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.confirmationModalFactory);

sabio.ng.addController(sabio.ng.app.module
, "$responseModalController"
, ['$scope', '$modalInstance', 'items', '$officeHoursService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.responseModalFactory);

sabio.ng.addController(sabio.ng.app.module
, "officeHoursDetailsController"
, ['$scope', '$baseController', '$officeHoursService', '$routeParams', '$location', '$route', '$templateCache', '$utilityService', '$uibModal']
, sabio.page.officeHoursDetailsControllerFactory);
