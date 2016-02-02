sabio.page.adminSectionsTabsControllerFactory = function (
    $baseController
    , $scope
    , $routeParams
    , $location
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;

    vm.selectTab = _selectTab;

    vm.sectionId = false;
    vm.tabs = null;
    vm.tabsUpdate = [
        { id: 1, label: 'Basic Info', tab: '', link:"#/" },
        { id: 2, label: 'Description', tab: 'description' },
        { id: 3, label: 'Location', tab: 'location' },
        { id: 4, label: 'Instructors', tab: 'instructors' },
        { id: 5, label: 'Enrollment', tab: 'enrollment' },
        { id: 6, label: 'Modules', tab: 'modules' }
    ];
    vm.tabsCreate = [
        vm.tabsUpdate[0]
    ]
    vm.currentTab = null;

    _init();

    vm.$scope.$on('$routeChangeSuccess', _onRouteChangeSuccess);

    function _init() {

    }

    function _selectTab(tab) {
        vm.currentTab = tab;
    }

    function _onRouteChangeSuccess(ev, current, prev) {

        //console.log("route change in tabs", current.params);
        switch(current.$$route.controller)
        {
            case "adminSectionsIndexController":
                vm.tabs = null;
                break;

            default:
                if (current.params.sectionId) {
                    vm.tabs = vm.tabsUpdate;
                    if (!vm.sectionId) {
                        vm.sectionId = current.params.sectionId;

                        for (var x = 0; x < vm.tabs.length; x++) {
                            vm.tabs[x].link = '#/manage/' + vm.sectionId + '/' + vm.tabs[x].tab;

                            if(current.$$route.originalPath.indexOf(vm.tabs[x].tab) > -1)
                            {
                                _selectTab(vm.tabs[x]);
                            }
                        }
                    }
                }
                else {
                    vm.tabs = vm.tabsCreate;
                }
                break;
        }
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsTabsController"
, ['$baseController', '$scope', '$routeParams', '$location']
, sabio.page.adminSectionsTabsControllerFactory);