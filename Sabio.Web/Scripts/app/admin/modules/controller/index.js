sabio.page.modulesListControllerFactory = function (
    $scope
    , $baseController
    , $modulesService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {

    var vm = this;
    vm.$scope = $scope;
   
    vm.$modulesService = $modulesService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;

    $baseController.merge(vm, $baseController);

    vm.notify = vm.$modulesService.getNotifier($scope);

    vm.onGetModuleListSuccess = _onGetModuleListSuccess;
    vm.onGetModuleListError = onGetModuleListError;
    vm.deleteModule = _deleteModule;
    vm.onDeleteSuccess = _onDeleteSuccess;
    vm.onDeleteError = _onDeleteError;
    vm.reloadTemplate = _reloadTemplate;

    render();

    function render() {
        vm.$modulesService.getList(vm.onGetModuleListSuccess, vm.onGetModuleListError);
    }

    function _deleteModule(id) {
        console.log(id);
        vm.$modulesService.deleteModule(id, vm.onDeleteSuccess, vm.onDeleteError);
        vm.reloadTemplate();
    }

    function _onGetModuleListSuccess(data) {
        vm.notify(function () {
            vm.mList = data.items;
        });
    }

    function onGetModuleListError(response) {
        console.log(response);
    }

    function _onDeleteSuccess(response) {
        console.log(response);
    }
    
    function _onDeleteError(response) {
        console.log(response)
    }

    function _reloadTemplate() {

        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }
}

sabio.ng.addController(sabio.ng.app.module
, "modulesListController"
, ['$scope', '$baseController', '$modulesService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.modulesListControllerFactory);