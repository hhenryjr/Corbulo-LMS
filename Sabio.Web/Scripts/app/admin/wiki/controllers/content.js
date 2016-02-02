sabio.page.adminWikiContentControllerFactory = function (
    $scope
    , $baseController
    , $wikiService
    , $wikiContentService
    , $routeParams
    , $uibModal
    , $alertService
    , $timeout
    ) {
    var vm = this;
    $baseController.merge(vm, $baseController);
    vm.$scope = $scope;
    vm.$scope.animationsEnabled = true;
    vm.$wikiService = $wikiService;
    vm.$wikiContentService = $wikiContentService;
    vm.$alertService = $alertService;
    vm.$routeParams = $routeParams;
    vm.$timeout = $timeout;    
    vm.notify = vm.$wikiService.getNotifier($scope);

    vm.addContent = _addContent;
    vm.selectContent = _selectContent;
    vm.updateContent = _updateContent;
    vm.deleteContent = _deleteContent;
    vm.onDropMoved = _onDropMoved;
    vm.addListItem = _addListItem;
    vm.addContentBelow = _addContentBelow;    //  currently not used - can remove
    vm.addContentAbove = _addContentAbove;
    vm.cancelAddContent = _cancelAddContent;
    vm.setDraggable = _setDraggable;

    vm.images = [];
    //vm.wikiContentId = 14;
    vm.listItem = [];
    vm.contentTypes = null;
    vm.syntaxes = null;
    vm.wikiPage = null;
    vm.wikiContent = null;
    vm.wikiOrder = null;
    vm.selectedContent = null;
    vm.deletedContent = null;
    vm.title = 'Add/Create Wiki Page';
    vm.pageContent = {};
    vm.hoverAdd = false;    //  currently not used
    vm.draggable = false;
    vm.addingContent = {
        above: false,
        below: false
    };

    vm.ckeditorOptions = {
        language: 'en',
        allowedContent: true,
        entities: false
    };
    vm.dropzoneConfig = {
        uploadMultiple: false,
        maxFileSize: 10,
        url: "/api/admin/wiki/content/" + vm.wikiContentId, //USE API FROM WikiContentService; the InsertContent(WikiContentCreateRequest request) method
        addRemoveLinks: true,
        init: _dropzoneInit
    };
    _init();

    function _init() {
        console.log("content init -> route params", vm.$routeParams);
        if (vm.$routeParams.wikiPageId) {
            //  go look up main wiki page info
            vm.title = 'Edit Content for Wiki Page #' + vm.$routeParams.wikiPageId;
            vm.$wikiService.get(vm.$routeParams.wikiPageId, _onGetWikiSuccess, _onGetWikiError);
        }
        vm.contentTypes = JSON.parse($("#wikiContentTypes").html());
        vm.syntaxes = JSON.parse($("#wikiHighlightLanguages").html());
        _cancelAddContent();
        //vm.pageContent = null;
    }

    function _addContent() {

        var data = vm.pageContent;

        if (vm.addingContent.below) {
            data.sortOrder = vm.addingContent.below.sortOrder + 10;
        }
        else if (vm.addingContent.above) {
            data.sortOrder = vm.addingContent.above.sortOrder;
        }

        vm.$wikiContentService.add(data, _onAddWikiSuccess, _onAddWikiError);
    }

    function _setDraggable(val) {
        vm.draggable = val;

        if (val)
            _cancelAddContent();
    }

    function _cancelAddContent() {
        vm.addingContent.below = false;
        vm.addingContent.above = false;
    }

    function _addContentBelow(content) {
        vm.addingContent.below = content;

        if (vm.addingContent.above)
            vm.addingContent.above = false;

        _scrollTo("#addContentWidget");
    }
    function _addContentAbove(content) {
        vm.addingContent.above = content;

        if (vm.addingContent.below)
            vm.addingContent.below = false;

        _scrollTo("#addContentWidget");
    }

    function _scrollTo(selector)
    {        
        vm.$timeout(function () {

            $('html, body').stop().animate({
                scrollTop: ($(selector).offset().top - 50)
            }, 500);

            //var widget = angular.element(id);
            //console.log("widget", widget);

            //var options = {
            //    duration: 500,
            //    easing: false,
            //    offset: 200,
            //    callbackBefore: function (element) {
            //        console.log('about to scroll to element', element);
            //    },
            //    callbackAfter: function (element) {
            //        console.log('scrolled to element', element);
            //    }
            //}
            //vm.smoothScroll(widget);
        }, 100);
    }

    function _onDropMoved(event, index) {

        if (vm.draggable)
        {
        vm.wikiContent.splice(arguments[1], 1);
        vm.wikiOrder = [];
        var order = 0;
        angular.forEach(vm.wikiContent, function (value, key) {
            vm.wikiOrder.push({
                id: value.id,
                order: order += 10
            });
        });

        console.log("order array", vm.wikiOrder);
        vm.$wikiContentService.order(vm.wikiOrder, _onOrderWikiSuccess, _onOrderWikiError);
    }
        else
        {            
            vm.$alertService.onWarning("That only works in Order mode.");

            _init();
        }
    }
    function _onOrderWikiSuccess(response) {
        vm.$alertService.onSuccess("Content order saved.");

        _init();
    }
    function _onOrderWikiError(response) {
        console.error("error ordering wiki content", response);
    }
    function _deleteContent(wikiContentId) {
        var msg = "Are you sure you want to delete content #" + wikiContentId + "?";

        if (confirm(msg)) {
            vm.$wikiContentService.deleteContent(wikiContentId, _onDeleteSuccess, _onDeleteError);
            vm.deletedContent = wikiContentId;
        }
    }
    function _onDeleteSuccess() {
        vm.$alertService.onSuccess("Content deleted.");

        _init();
    }
    function _onDeleteError(response) {
        console.error("error deleting wiki content", response);

        vm.$alertService.onError("Could not delete content. Please try again.")
    }
    function _selectContent(content) {
        vm.selectedContent = content;
        console.log("selected content", vm.selectedContent);
        if (!vm.selectedContent.contentData) 
            vm.selectedContent.contentData = [];

        if (vm.selectedContent.contentData.length == 0)
            vm.selectedContent.contentData.push({ value: "" });
        
    }
    function _updateContent() {
        if (vm.selectedContent.contentData) {
            var values = vm.selectedContent.contentData;
            var log = [];
            for (var x = 0; x < values.length; x++) {
                // must create a temp object to set the key using a variable
                var tempObj = {};
                tempObj[x] = values[x];
                log.push(tempObj);
            }

            //angular.forEach(values, function (value) {
            //    this.push(value);
            //}, log);
            //vm.selectedContent.contentData = log;
        }
        vm.$wikiContentService.update(vm.selectedContent.id, vm.selectedContent, _onUpdateWikiSuccess, _onUpdateWikiError);
    }
    function _onUpdateWikiSuccess(response) {
        vm.selectedContent = null;
        vm.$alertService.onSuccess("Content updated.");
        _init();
    }
    function _onUpdateWikiError(response) {
        console.error("error updating wiki content", response);
        vm.$alertService.onError("There was an error updating the wiki content.");
    }

    function _onAddWikiSuccess(response) {
        vm.$alertService.onSuccess("New content block added.");
        vm.pageContent.contentTypeId = null;
        vm.pageContent.title = null;
        _init();
    }
    function _onAddWikiError(response) {
        vm.$alertService.onError("Error adding wiki. Please be sure to select a Type!");
        console.error("error adding wiki", response);
    }
    function _onGetWikiSuccess(response) {
        var wiki = response.item;
        if (wiki.publishDate)
            wiki.publishDate = new Date(wiki.publishDate);

        vm.notify(function () {
            vm.wikiPage = wiki;
            vm.pageContent.wikiPageId = vm.wikiPage.id
        });

        if (vm.$routeParams.moduleId) {
            vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
                title: "Edit Module <strong>" + vm.wikiPage.title + "</strong> ",
                subtitle: "Sections Manager",
                buttons: [
                    { link: "#/manage/" + vm.$routeParams.sectionId + "/modules", icon: "fa-arrow-left", text: "Back to Module" }
                    , { link: "#/manage/" + vm.$routeParams.sectionId + "/modules/" + vm.$routeParams.moduleId + "/wiki/edit/" + vm.wikiPage.id, icon: "fa-pencil", text: "Edit Module Wiki Page" }
                ]
            });
        }
        else {
            vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
                title: "Edit Wiki Content <strong>" + vm.wikiPage.title + "</strong> ",
                subtitle: "Wiki Manager",
                buttons: [
                    { link: "#/", icon: "fa-arrow-left", text: "Back to List" }
                    , { link: "#/edit/" + vm.wikiPage.id, icon: "fa-pencil", text: "Edit Page" }
                ]
            });
        }

        vm.$wikiContentService.getByPageId(vm.$routeParams.wikiPageId, _onGetWikiContentSuccess, _onGetWikiContentError);
    }
    function _onGetWikiError(response) {
        console.error("error getting wiki for display", response);
        vm.$alertService.onError("Unable to retrive wiki data.");
    }
    function _onGetWikiContentSuccess(response) {
        vm.notify(function () {
            vm.wikiContent = response.items;
        });
    }
    function _onGetWikiContentError(response) {
        console.error("error getting wiki content", response);

        vm.$alertService.onError("Error getting wiki content.");
    }
    function _onUploadSuccess() {

        vm.$alertService.onSuccess("File has been saved.");

        var response = arguments[1];

        vm.selectedContent = null;

        _init();
    }
    function _onUploadProcess(file) {
        this.options.url = "/api/files/wikiContent/" + vm.selectedContent.id;
    }
    function _dropzoneInit() {

        //this.on("addedfile", _onAddedFile);
        this.on("success", _onUploadSuccess);
        this.on("processing", _onUploadProcess);

        //  based on http://stackoverflow.com/a/25179408
        if (vm.images) {
            for (var i = 0; i < vm.images.length; i++) {
                var pic = vm.images[i];

                var row = {
                    //name: pic.title,
                    //size: pic.size,
                    type: pic.items[0].fileTypeId,
                    status: Dropzone.ADDED,
                    url: pic.items[0].path,
                    id: pic.items[0].fileId
                };

                // Call the default addedfile event handler
                this.emit("addedfile", row);

                // And optionally show the thumbnail of the file:
                this.emit("thumbnail", row, row.url);

                this.files.push(row);
            }
        }
    }
    function _addListItem() {
        if (!vm.selectedContent.contentData)
            vm.selectedContent.contentData = [];

        vm.selectedContent.contentData.push({ value: "" });
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminWikiContentController"
, ['$scope', '$baseController', '$wikiService', '$wikiContentService', '$routeParams', '$uibModal', '$alertService', '$timeout']
, sabio.page.adminWikiContentControllerFactory);