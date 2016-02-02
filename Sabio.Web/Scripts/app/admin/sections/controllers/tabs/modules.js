/// <reference path="../../templates/tabs/module-modal.html" />
/// <reference path="../../templates/tabs/module-modal.html" />
/// <reference path="../../templates/tabs/module-modal.html" />
sabio.page.adminSectionsModulesControllerFactory = function (
    $scope
    , $baseController
    , $routeParams
    , $modulesService
    , $wikiTreeService
    , $sectionsService
    , $uibModal
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$modulesService = $modulesService;
    vm.$wikiTreeService = $wikiTreeService;
    vm.$sectionsService = $sectionsService;
    vm.notify = vm.$modulesService.getNotifier($scope);

    vm.createModule = _createModule;
    vm.loadModulePages = _loadModulePages;

    vm.onGetSuccess = _onGetSuccess;
    vm.onGetError = _onGetError;


    vm.module = null;
    vm.section = null;
    vm.selectedModule = null;
    vm.sectionModules = null;
    vm.moduleForm = null;
    vm.showModuleFormErrors = false;
    vm.wikiTree = null;
    vm.wikiTreeOptions = {
        dropped: _onTreeNodeDropped
    };
    vm.openModule = _openModule;

    _init();

    function _init() {
        console.log("modules route params", vm.$routeParams);
        vm.$modulesService.getList(vm.onGetSuccess, vm.onGetError);
        _loadSection();

    }

    function _loadModulePages(module) {
        vm.selectedModule = module;
        vm.wikiTree = null;

        vm.$modulesService.getModuleTree(vm.selectedModule.id, _onGetPagesSuccess, _onGetPagesError);

    }

    function _openModule(sectionId) {
        vm.sectionId = [];
        vm.sectionId.push(sectionId)
        console.log(sectionId);
        var modalSettings = {
            animation: $scope.animationsEnabled,
            templateUrl: '/scripts/app/admin/sections/templates/tabs/module-modal.html',
            controller: 'moduleModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return vm.items;
                },
                sectionId: function () {
                    return vm.sectionId;
                }
            }
        };
        var modalInstance = $uibModal.open(modalSettings);
    }

    function _loadSection() {
        if (vm.$routeParams.sectionId)
            vm.$sectionsService.getSection(vm.$routeParams.sectionId, _onGetSectionSuccess, _onGetSectionError);
    }

    function _onGetSectionSuccess(response) {

        vm.notify(function () {
            vm.section = response.item;
        });

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
            title: "Section #" + vm.section.id + " <strong>(" + vm.section.course.courseName + ") </strong>",
            subtitle: "Sections Manager",
            buttons: [
                { link: "#/", icon: "fa-arrow-left", text: "Back to List" }
            ]
        });

        _loadModules();
    }

    function _onGetSectionError(response) {
        console.error("error getting section", response);
    }

    function _onTreeNodeDropped(e) {
        //console.log("tree node was dropped", e);
        var pageId = (e.source.nodeScope.$modelValue.id);
        var newParentId = 0;

        if (e.dest.nodesScope.$parent.$modelValue)
            newParentId = e.dest.nodesScope.$parent.$modelValue.id;

        vm.$wikiTreeService.updateParentId(pageId, newParentId, _onParentIdSuccess, _onParentError);
    }

    function _onParentIdSuccess(response) {
        console.log("parent id success", response);
    }

    function _onParentError(jqXhr, error) {
        console.log("parent id error", jqXhr);
    }

    function _onGetPagesSuccess(response) {
        vm.notify(function () {
            vm.wikiTree = response.items;

            console.log("loaded module tree", vm.wikiTree);
        });
    }

    function _onGetPagesError(response) {
        console.error("error getting pages", response);
    }

    function _onGetError(response, error) {
        console.error(error);
    }

    function _onGetSuccess(response) {
        vm.notify(function () {
            vm.items = response.items;

        }
        )
    }

    function _createModule() {
        vm.showModuleFormErrors = true;

        if (vm.moduleForm.$valid) {
            var data = vm.module;

            data.sectionId = vm.$routeParams.sectionId;

            vm.$modulesService.addModule(data, _onCreateModuleSuccess, _onCreateModuleError)

            console.log("create module VALID", vm.module);
        }
    }

    function _loadModules() {
        vm.$modulesService.getSectionModules(vm.$routeParams.sectionId, _onGetModulesSuccess, _onGetModulesError)
    }

    function _onGetModulesSuccess(response) {
        vm.notify(function () {
            vm.sectionModules = response.items;
        });
    }

    function _onGetModulesError(response) {
        console.error("error getting modules", response);
    }

    function _onCreateModuleSuccess(response) {
        vm.notify(function () {
            vm.module = null;
            vm.showModuleFormErrors = false;
        });

        _loadModules();
    }

    function _onCreateModuleError(response) {
        console.error("error creating module", response);
    }
}

sabio.page.moduleModalControllerFactory = function (
   $scope
   , $modalInstance
   , items
   , sectionId
   , $moduleService
   ) {
    var vm = this;
    vm.$scope = $scope;
    vm.modules = items;
    vm.sectionId = sectionId;


    vm.submitData = _submitData;
    vm.createModuleSuccess = _createModuleSuccess;
    vm.createModuleError = _createModuleError;
    vm.modalInstance = $modalInstance;
    vm.$moduleService = $moduleService;
    vm.selected = {
        items: vm.modules[0]
    }
    console.log(vm.modules);
    function _submitData(moduleId) {
        console.log(moduleId, vm.sectionId);
        vm.$moduleService.addSectionModule(moduleId, vm.sectionId, vm.createModuleSuccess, vm.createModuleError);

    }

    function _createModuleSuccess(response) {
        console.log(response.item);
        vm.notify = function () {
            vm.newModules = response.item;
        }
    }

    function _createModuleError(response, error) {
        console.log(error);
    }

    vm.cancel = function () {
        $modalInstance.close();
    }
}


sabio.ng.addController(sabio.ng.app.module
, "adminSectionsModulesController"
, ['$scope', '$baseController', '$routeParams', '$modulesService', '$wikiTreeService', '$sectionsService', '$uibModal']
, sabio.page.adminSectionsModulesControllerFactory);

sabio.ng.addController(sabio.ng.app.module
, "moduleModalController"
, ['$scope', '$modalInstance', 'items', 'sectionId', '$modulesService']
, sabio.page.moduleModalControllerFactory);

