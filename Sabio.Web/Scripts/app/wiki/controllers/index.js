sabio.page.wikiIndexControllerFactory = function (
    $scope
    , $baseController
    , $wikiTreeService
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$wikiTreeService = $wikiTreeService;
    vm.notify = vm.$wikiTreeService.getNotifier($scope);

    vm.wikiSpaces = null;
    vm.wikiPageType = 1;

    _init();

    function _init() {

        if ($("#wikiSpacesData").length)
        {
            vm.wikiSpaces = JSON.parse($("#wikiSpacesData").html());
        }        
        console.log("index wiki spaces", vm.wikiSpaces);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "wikiIndexController"
, ['$scope', '$baseController', '$wikiTreeService']
, sabio.page.wikiIndexControllerFactory);