sabio.page.adminSectionsLocationControllerFactory = function (
    $scope
    , $baseController
    , $sectionsService
    , $routeParams
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$sectionsService = $sectionsService;
    vm.$routeParams = $routeParams;
    vm.notify = vm.$sectionsService.getNotifier($scope);

   
    vm.onCampusesGetSuccess = _onCampusesGetSuccess;
    vm.onCampusesGetError = _onCampusesGetError;
    vm.onUpdateLocationSuccess = _onUpdateLocationSuccess;
    vm.onUpdateLoctionError = _onUpdateLoctionError;

    vm.section = {};
    vm.campuses = [];

    vm.update = _update;

    _init();

    function _init() {
        vm.$sectionsService.getCampuses(_onCampusesGetSuccess, _onCampusesGetError)
        vm.$sectionsService.getSection( vm.$routeParams.sectionId,_onGetSectionsSuccess, _onGetSectionsError);
    }   
    function _onCampusesGetSuccess(response) {
        vm.notify(function () {
            vm.campuses = response.items;
        });
    }

    function _onCampusesGetError(response) {
        console.error("error getting campuses", response);
    }

    function _update() {
         vm.$sectionsService.updateLocation(vm.$routeParams.sectionId, vm.section, _onUpdateLocationSuccess, _onUpdateLoctionError)
    }

    function _onUpdateLocationSuccess(data) {
        console.log(data);
    }

    function _onUpdateLoctionError(response) {
        console.error("error updating info", response);
    }

    function _onGetSectionsSuccess(response)
    {
        vm.notify(function () {
            vm.section = response.item;
        });    
    }

    function _onGetSectionsError(response) {
        console.error("error getting sections", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsLocationController"
, ['$scope', '$baseController', '$sectionsService', '$routeParams']
, sabio.page.adminSectionsLocationControllerFactory);