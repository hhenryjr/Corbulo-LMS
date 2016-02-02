sabio.page.wikiPageControllerFactory = function (
    $scope
    , $routeParams
    , $baseController
    , $wikiContentService
    , $eventHandlerService
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$wikiContentService = $wikiContentService;
    vm.$eventHandlerService = $eventHandlerService;
    vm.notify = vm.$wikiContentService.getNotifier($scope);

    vm.wikiPage = null;
    vm.spaceId = null;
    vm.currentSpace = null;
    vm.contentTypes = null;

    _init();

    function _init() {
        vm.spaceId = vm.$routeParams.spaceId;

        vm.$eventHandlerService.broadcast("loadSpace", vm.spaceId);

        vm.contentTypes = JSON.parse($("#wikiContentTypes").html());

        vm.$wikiContentService.getPageBySlug(vm.$routeParams.pageSlug, _onGetWikiContentSuccess, _onGetWikiContentError);
    }

    function _onGetWikiContentSuccess(response) {

        var page = response.item;
        var space = null;

        if(page.wikiSpaces && page.wikiSpaces.length)
        {
            for(var x=0;x<= page.wikiSpaces.length;x++)
            {
                var thisSpace = page.wikiSpaces[x];

                if(thisSpace.id == vm.spaceId)
                {
                    space = thisSpace;
                    break;
                }
            }
        }

        vm.notify(function () {
            vm.wikiPage = page;
            vm.currentSpace = space;
        });
    }
   

    function _onGetWikiContentError(response) {
        console.error("error getting wiki page content", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "wikiPageController"
, ['$scope', '$routeParams', '$baseController', '$wikiContentService', '$eventHandlerService']
, sabio.page.wikiPageControllerFactory);