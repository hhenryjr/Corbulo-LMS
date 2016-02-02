sabio.page.adminWikiTreeControllerFactory = function (
    $scope
    , $routeParams
    , $baseController
    , $wikiService
    , $wikiTreeService
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$wikiService = $wikiService;
    vm.$wikiTreeService = $wikiTreeService;
    vm.notify = vm.$wikiService.getNotifier($scope);

    vm.loadSpace = _loadSpace;

    vm.wikiSpaces = null;
    vm.currentSpace = null;
    vm.wikiTree = null;
   
    _init();

    function _init() {
        console.log("init tree params", vm.$routeParams);

        if (vm.$routeParams.spaceId)
        {
            _loadSpace({ id: vm.$routeParams.spaceId })
        }
        else
        {
            vm.$wikiTreeService.getAllSpaces(_onGetSpacesSuccess, _onGetSpacesError)
        }

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
            title: "Wiki Pages <strong>Tree</strong> ",
            subtitle: "Wiki Manager",
            buttons: [
                { link: "#/", icon: "fa-list", text: "List View" }
            ]
        });
    }

    function _loadSpace(space)
    {
        vm.currentSpace = space;
        vm.wikiTree = null;

        console.log("load space", space);

        vm.$wikiTreeService.getSpaceById(vm.currentSpace.id, _onGetTreeSuccess, _onGetTreeError)        
    }

    function _onGetTreeSuccess(response) {
        vm.notify(function () {
            vm.wikiTree = response.items;
        });
    }

    function _onGetTreeError(response) {
        console.error("error getting tree", response);
    }

    function _onGetSpacesSuccess(response)
    {
        vm.notify(function () {
            vm.wikiSpaces = response.items;
        });
        
        console.log("spaces from server", vm.wikiSpaces);
    }

    function _onGetSpacesError(response) {
        console.error("error getting spaces", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminWikiTreeController"
, ['$scope', '$routeParams', '$baseController','$wikiService','$wikiTreeService']
, sabio.page.adminWikiTreeControllerFactory);