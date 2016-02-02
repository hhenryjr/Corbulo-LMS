sabio.page.adminWikiIndexControllerFactory = function (
    $scope
    , $baseController
    , $wikiService
    , $routeParams) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
   
    vm.$wikiService = $wikiService;
    vm.$routeParams = $routeParams;

    vm.notify = vm.$wikiService.getNotifier($scope);

    vm.getPagesByType = _getPagesByType;
    vm.onReceiveSpaces = _onReceiveSpaces;
    vm.onSpaceError = _onSpaceError;

    vm.getSpaceByID = _getSpaceByID;

    vm.onReceiveSpaceById = _onReceiveSpaceById;
    vm.onSpaceByIdError = _onSpaceByIdError;

    vm.delete = _delete;
    vm.onPageClickNext = _onPageClickNext;
    vm.onPageClickPrevious = _onPageClickPrevious;

    vm.wikiPages = [];
    vm.wikiPageType = 1;

    vm.currentPage = 1;

    vm.totalitems = [];
    vm.pagination = {
        "pageSize": 10,
        "pageIndex": 0
    };


    _init();
    function _init() {
        vm.getPagesByType();
        vm.$wikiService.getAllSpaces(vm.onReceiveSpaces, vm.onSpaceError);

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
            title: "Wiki Pages <strong>List</strong> ",
            subtitle: "Wiki Manager",
            buttons: [
                { link: "#/tree", icon: "fa-tree", text: "Tree View" }
            ]
        });
    }
        
    function _getPagesByType() {
        vm.$wikiService.getByType(vm.pagination, vm.wikiPageType, _onGetWikisSuccess, _onGetWikisError);
    }

    function _delete(pageId) {
        alert("TODO: go delete page #" + pageId);
    }

    function _onGetWikisSuccess(response) {
        console.log(response);
        vm.notify(function () {
            vm.wikiPages = response.item.pagedItems;
            vm.totalitems = response.item.totalCount;
        });
    }

    function _onGetWikisError(response) {
        console.log(response);
    }

    function _onReceiveSpaces(data) {
        vm.notify(function () {
            vm.spaces = data.items;
        });
    }

    function _onSpaceError(jqXhr, error) {
        console.log(error);
    }

    function _getSpaceByID() {
        var id = vm.wikiPages.id;
        vm.$wikiService.getSpaceById(id, vm.onReceiveSpaceById, vm.onSpaceByIdError);
    }

    function _onReceiveSpaceById(data) {
        vm.notify(function () {
            vm.wikiPages = data.items;
        });
    }

    function _onSpaceByIdError(jqXhr, error) {
        console.log(error);
    }

    function _onPageClickPrevious() {
        vm.pagination.pageIndex -= 1;   
        vm.$wikiService.getByType(vm.pagination, vm.wikiPageType, _onGetWikisSuccess, _onGetWikisError);
    }

    function _onPageClickNext() {  
        vm.pagination.pageIndex += 1;
        vm.$wikiService.getByType(vm.pagination, vm.wikiPageType, _onGetWikisSuccess, _onGetWikisError);
    }
}


sabio.ng.addController(sabio.ng.app.module
, "adminWikiIndexController"
, ['$scope', '$baseController', '$wikiService', '$routeParams']
, sabio.page.adminWikiIndexControllerFactory);