sabio.page.sectionsIndexControllerFactory = function (
    $scope
    , $baseController
    , $sectionsService
    ){

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$sectionsService = $sectionsService;
    vm.notify = vm.$sectionsService.getNotifier($scope);

    vm.sections = null;

    vm.headerContent = {
        title: "Manage <strong>Sections</strong>",
        subtitle: "Learning Command System"
    };

    _init();

    function _init() {

        vm.$sectionsService.getSections(_onGetSectionsSuccess, _onGetSectionsError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, vm.headerContent);
    }   

    function _onGetSectionsSuccess(response)
    {
        vm.notify(function () {
            vm.sections = response.items;
        });
        
        console.log("sections from server", vm.sections);
    }

    function _onGetSectionsError(response) {
        console.error("error getting sections", response);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsIndexController"
, ['$scope', '$baseController', '$sectionsService']
, sabio.page.sectionsIndexControllerFactory);