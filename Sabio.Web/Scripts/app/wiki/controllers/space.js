sabio.page.wikiSpaceControllerFactory = function (
    $scope
    , $routeParams
    , $baseController
    , $wikiTreeService
    , $eventHandlerService
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$wikiTreeService = $wikiTreeService;
    vm.$eventHandlerService = $eventHandlerService;
    vm.notify = vm.$wikiTreeService.getNotifier($scope);

    
    vm.wikiPageType = 1;
    vm.spaceId = null;

    _init();

    function _init() {

        vm.spaceId = vm.$routeParams.spaceId;

        vm.$eventHandlerService.broadcast("loadSpace", vm.spaceId);        
    }
}

sabio.ng.addController(sabio.ng.app.module
, "wikiSpaceController"
, ['$scope', '$routeParams', '$baseController', '$wikiTreeService','$eventHandlerService']
, sabio.page.wikiSpaceControllerFactory);