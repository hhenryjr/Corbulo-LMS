sabio.page.commentControllerFactory = function (
    $scope
    , $routeParams
    , $baseController
    , $commentService
    , $location
    , $route
    , $templateCache
    , $uibModal
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.$commentService = $commentService;

    vm.notify = vm.$commentService.getNotifier($scope);
    vm.animationsEnabled = true;
    vm.onSuccess = _onSuccess;
    vm.onError = _onError;

    vm.wikiPageType = 1;
    vm.wikiId = null;

    vm.parentId = 0;
    vm.ownerTypeId = 4;
    vm.ownerId = 1;

    _init();

    function _init() {
        // use to get page id and send it back to fetch data for this page 
        //vm.ownerId = vm.$routeParams.wikiPageId; 
        vm.$eventHandlerService.broadcast("loadSpace", vm.spaceId);
        vm.$commentService.getCommentsByOwnerTypeAndParentId(vm.ownerId, vm.ownerTypeId, vm.parentId, _onSuccess, _onError);

    }

    function _onSuccess(response) {
        console.log(response);
        vm.notify(function () {
            vm.comments = response.items;
        });

    }
    function _onError(response) {
        console.log(response);
    }

    vm.open = function () {
        var commentModal = {
            animation: vm.animationsEnabled,
            templateUrl: 'modal-comment-reply.html',
            controller: '$modalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return{parentId: vm.parentId = 0,
                   ownerTypeId: vm.ownerTypeId = 4,
                  ownerId: vm.ownerId = 1,}
                }
            }
        };
        var contactModalInstance = $uibModal.open(commentModal);
    }



    
}

sabio.ng.addController(sabio.ng.app.module
, "commentsController"
, ['$scope', '$routeParams', '$baseController', '$commentService', '$location', '$route', '$templateCache', '$uibModal']
, sabio.page.commentControllerFactory)


sabio.services.modalFactory = function ($baseService) {
    var aSabioServiceObject = sabio.services.comments;
    var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);
    return newService;
}

sabio.page.modalControllerFactory = function (
    $scope,
    $modalInstance,
    $modalService,
    $location,
    $route,
    $templateCache,
    $routeParams,
    items
    ) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $modalInstance;
    vm.$modalService = $modalService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.items = items;
  
    vm.comment = {};
    vm.addComment = _addComment;
    vm.onSuccess = _onSuccess;
    vm.onError = _onError;
    vm.reloadTemplate = _reloadTemplate;

    vm.cancel = function () {
        $modalInstance.close();
    }

    function _addComment(cmt) {
        var body = vm.comment.body;
        var title = vm.comment.title;
        var  parentId = vm.items.parentId;
        var ownerTypeId = vm.items.ownerTypeId;
        var ownerId = vm.items.ownerId;

        var commentData = { body, title, parentId, ownerTypeId, ownerId };

        vm.$modalService.addComment(commentData, vm.onSuccess, vm.onError)
        $modalInstance.close();
        _reloadTemplate();
        
    }


    function _onSuccess(response) {
        console.log(response);
      
    }
    function _onError(response) {
        console.log(response);
    }

    function _reloadTemplate() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }

}

sabio.ng.addService(sabio.ng.app.module
            , "$modalService"
            , ["$baseService"]
            , sabio.services.modalFactory);


sabio.ng.addController(sabio.ng.app.module
            , "$modalController"
            , ['$scope', '$modalInstance', '$modalService', '$routeParams', '$location', '$route', '$templateCache','items']
            , sabio.page.modalControllerFactory);