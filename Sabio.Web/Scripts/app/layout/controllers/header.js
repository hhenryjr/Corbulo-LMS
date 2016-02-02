sabio.page.layoutHeaderControllerFactory = function (
    $scope
    , $baseController
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;

    vm.content = {
        title: "Default <strong>Title</strong>",
        subtitle: "Learning Command System",
        buttons:false
    };

    _init();

    function _init() {
        vm.$eventHandlerService.listen(vm.$eventsEnum.HEADER_DATA, _onHeaderData);
    }

    function _onHeaderData(event, args){        
        vm.content = args[1];
    }
}

sabio.ng.addController(sabio.ng.app.module
, "layoutHeaderController"
, ['$scope', '$baseController']
, sabio.page.layoutHeaderControllerFactory);