sabio.page.adminSectionsDescriptionControllerFactory = function (
    $scope
    , $baseController
    , $sectionsService
    , $routeParams
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$sectionsService = $sectionsService;
    vm.$routeParams = $routeParams;
    vm.notify = vm.$sectionsService.getNotifier($scope);
   
    vm.updateInfo = _updateInfo;
    vm._onUpdateInfoSuccess = _onUpdateInfoSuccess;
    vm.onUpdateInfoError = _onUpdateInfoError;

    vm.section = {};

    _init();

    function _init() {

        vm.$sectionsService.getSection(vm.$routeParams.sectionId,_onGetSectionsSuccess, _onGetSectionsError);
    }

    function _onGetSectionsSuccess(response) {
        vm.notify(function () {
            vm.section = response.item;
        });
    }

    function _onGetSectionsError(response) {
        console.error("error getting sections", response);
    }

    function _updateInfo() {    
        vm.$sectionsService.updateInfo(vm.$routeParams.sectionId, vm.section, _onUpdateInfoSuccess, _onUpdateInfoError)
    }

    function _onUpdateInfoSuccess(data) {
        console.log(data);
    }

    function _onUpdateInfoError(response) {
        console.error("error updating info", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsDescriptionController"
, ['$scope', '$baseController', '$sectionsService', '$routeParams']
, sabio.page.adminSectionsDescriptionControllerFactory);