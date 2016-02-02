sabio.page.modulesManagerControllerFactory = function (
    $scope
    , $baseController
    , $modulesService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$modulesService = $modulesService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;

    vm.settings = _settings;
    vm.backToList = _backToList;
    
    vm.modUpdateSuccess = _modUpdateSuccess;
    vm.modUpdateError = _modUpdateError;

    vm.addModSuccess = _addModSuccess;
    vm.onAddModError = _onAddModError;
   
    vm.addSpaces = _addSpaces;
   
    vm.onValidateForm = _onValidateForm;
    vm.submitMyData = _submitMyData;

    vm.onReceivePages = _onReceivePages;
    vm.onPagesError = _onPagesError;
  
    vm.CheckBoxClicked = _CheckBoxClicked;

    vm.onGetModuleSuccess = _onGetModuleSuccess;
    vm.onGetModuleError = _onGetModuleError;

    vm.onWikiPageAddSuccess = _onWikiPageAddSuccess;
    vm.onWikiPageAddError = _onWikiPageAddError;

    vm.onDelWPageSuccess = _onDelWPageSuccess;
    vm.onDelWPageError = _onDelWPageError;

    vm.resetModulesForm = _resetModulesForm;
    vm.disableAddButton = _disableAddButton;

    vm.getModuleWikisSuccess = _getModuleWikisSuccess;
    vm.getModuleWikisError = _getModuleWikisError;

    vm.addOrDeleteWikiPages = _addOrDeleteWikiPages;

    vm.notify = vm.$modulesService.getNotifier($scope);


    render();

    function render() {
        vm.settings();
        vm.$modulesService.getAllPages(vm.onReceivePages, vm.onPagesError);

        if (vm.$routeParams.moduleId) {
            vm.addModules = false;
            vm.editModules = true;
            vm.addWiki = false;
            //  edit mode - go look up record
            vm.title = 'Modules Editor';
            vm.$modulesService.getModuleDetail(vm.$routeParams.moduleId, vm.onGetModuleSuccess, vm.onGetModuleError);
            vm.$modulesService.getModuleWikis(vm.$routeParams.moduleId, vm.getModuleWikisSuccess, vm.getModuleWikisError);
        }
        else {
            //  create mode
            vm.title = 'Add a New Module';
        }
    }

    function _submitMyData() {
        console.log(vm.details);

        if (vm.$routeParams.moduleId) {
            vm.details.Id = vm.$routeParams.moduleId;
            vm.$modulesService.updateModule(vm.$routeParams.moduleId, vm.details, vm.modUpdateSuccess, vm.modUpdateError);
        } else if (vm.mId) {
            vm.details.Id = vm.mId;
            vm.$modulesService.updateModule(vm.mId, vm.details, vm.modUpdateSuccess, vm.modUpdateError);
        } else {
            vm.$modulesService.addModule(vm.details, vm.addModSuccess, vm.onAddModError);
            vm.disableAddButton();
        }
    }

    function _CheckBoxClicked(id, checked, key) {
        vm.activeTab = true;
        if (vm.$routeParams.moduleId) {
            vm.addOrDeleteWikiPages(id, checked, vm.$routeParams.moduleId);
        } else {
            vm.addOrDeleteWikiPages(id, checked, vm.mId);
        }
    }

    function _addOrDeleteWikiPages(wId, checked, mId) {

        if (checked === true) {
            vm.$modulesService.addWikiPage(mId, wId, vm.onWikiPageAddSuccess, vm.onWikiPageAddError);
        }
        else if (checked === false) {
            vm.$modulesService.deleteWikiPage(mId, wId, vm.onDelWPageSuccess, vm.onDelWPageError);
        }
        vm.$modulesService.getModuleWikis(mId, vm.getModuleWikisSuccess, vm.getModuleWikisError);
    }

    function _addModSuccess(data) {
        console.log(data);
        vm.mId = data.item;
    }

    function _onGetModuleSuccess(response) {

        var mod = response.item;
        vm.notify(function () {
            vm.details = mod;
            console.log(vm.details);
        });
    }

    function _onWikiPageAddSuccess(response){
        console.log("add success",response);
    }

    function _onGetModuleError(response) {
        console.error(response);
    }

    function _modUpdateSuccess(data) {
        console.log(data);
    }

    function _onDelWPageSuccess(response){
        console.log("delete success", response);
    }

    function _getModuleWikisSuccess(data) {
        vm.chosen = { Roles: [] };
        vm.notify(function () { 
            vm.moduleWikis = data.items;
            for (x = 0; x < vm.moduleWikis.length; x++)
                vm.chosen.Roles.push(vm.moduleWikis[x].wikiPageId)
        });
    }

    function _getModuleWikisError(response) {
        console.log(response);
    }

    function _onDelWPageError(response){
        console.log(response);
    }

    function _onWikiPageAddError(response){
        console.log(response);
    }

    function _modUpdateError(jqXhr, error) {
        console.log(error);
    }

    function _onAddModError(jqXhr, error) {
        console.log(error);
    }


    function _onReceivePages(data) {
        vm.notify(function () {
            vm.pageList = data.items;       
            vm.wPagelist = {};
            for (var i = 0; i < vm.pageList.length; i++) {                                   
                vm.wPagelist[vm.pageList[i].title] = vm.pageList[i].id;
            }
        });
    }

    function _addSpaces(data) {
        console.log(data);
    }

    function _onPagesError(jqXhr, error) {
        console.log(error);
    }
   

    function _backToList() {
        vm.$location.path('/modules#');
        vm.$location.replace();
    }

    function _settings() {
        vm.addModules = true;
        vm.editModules = false;
        vm.showFormErrors = false;
        vm.title = 'Add a Module';
        vm.addWiki = true;
        vm.details = {};
    }

    function _onValidateForm() {

        vm.showFormErrors = true;

        if (vm.modulesForm.$valid) {     
            vm.submitMyData();
        }
    };
  
    function _disableAddButton() {
        vm.addWiki = false;
        vm.activeTab = true;
        vm.editModules = true;
        vm.addModules = false;
    }

    function _resetModulesForm($event) {

        $event.stopPropagation();

        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
             
    }
}

sabio.ng.addController(sabio.ng.app.module
, "modulesManagerController"
, ['$scope', '$baseController', '$modulesService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.modulesManagerControllerFactory);