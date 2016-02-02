sabio.page.wikiNavControllerFactory = function (
    $scope
    , $location
    , $baseController
    , $wikiTreeService
    , $eventHandlerService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$location = $location;
    vm.$wikiTreeService = $wikiTreeService;
    vm.$eventHandlerService = $eventHandlerService;
    vm.notify = vm.$wikiTreeService.getNotifier($scope);

    vm.loadCurrentSpace = _loadCurrentSpace;
    vm.selectSpace = _selectSpace;

    vm.wikiTree = null;
    vm.wikiSpaces = null;
    vm.wikiPageType = 1;
    vm.currentSpaceId = null;

    _init();

    function _init() {

        vm.wikiSpaces = JSON.parse($("#wikiSpacesData").html());

        vm.$eventHandlerService.listen("loadSpace", _onLoadSpaceEvent);
    }

    function _selectSpace()
    {
        console.log("current space", vm.currentSpaceId);

        vm.$location.url("space/" + vm.currentSpaceId.id);
        _loadCurrentSpace();
    }

    function _onLoadSpaceEvent(event, args)
    {
        if (args && args[1])
        {
            vm.currentSpaceId = { id: args[1] };
            _loadCurrentSpace();
        }
    }

    function _loadCurrentSpace()
    {        
        vm.$wikiTreeService.getSpaceById(vm.currentSpaceId.id, _onGetTreeSuccess, _onGetTreeError)
    }

    function _onGetTreeSuccess(response) {
        vm.notify(function () {
            vm.wikiTree = response.items;
        });
    }

    function _onGetTreeError(response) {
        console.error("error getting tree", response);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "wikiNavController"
, ['$scope', '$location', '$baseController', '$wikiTreeService', '$eventHandlerService']
, sabio.page.wikiNavControllerFactory);