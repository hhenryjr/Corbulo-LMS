sabio.page.adminWikiManageControllerFactory = function (
    $scope
    , $baseController
    , $wikiService
    , $routeParams
    , $modulesService
    , $utilityService
    , $wikiContentService
    , $alertService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$wikiService = $wikiService;
    vm.$modulesService = $modulesService;
    vm.$routeParams = $routeParams;
    vm.$utilityService = $utilityService;
    vm.$wikiContentService = $wikiContentService;
    vm.$alertService = $alertService;
    vm.notify = vm.$wikiService.getNotifier($scope);

    vm.updateSuccess = _updateSuccess;
    vm.onUpdateError = _onUpdateError;
    vm.addSuccess = _addSuccess;
    vm.onAddError = _onAddError;
    vm.submit = _submit;
    vm.createSlug = _createSlug;
    vm.checkSlugUrl = _checkSlugUrl;
    //vm.applySlug = _applySlug;

    vm.spaceList = {};
    vm.chosenWikiSpaces = [];

    vm.onReceiveSpaces = _onReceiveSpaces;
    vm.onSpaceError = _onSpaceError;

    vm.addSpaces = _addSpaces;
    vm.checkboxClicked = _checkboxClicked;
    vm.onSpaceUpdateSuccess = _onSpaceUpdateSuccess;
    vm.onDeleteSpace = _onDeleteSpace;

    vm.tagAdded = _tagAdded;
    vm.tagRemoved = _tagRemoved;

    vm.onTagSuccess = _onTagSuccess;
    vm.onTagError = _onTagError;

    vm.slugExists = _getWikiBySlugSuccess;
    vm.noSlug = _getWikiBySlugError;

    vm.OnTagAddSuccess = _OnTagAddSuccess;
    vm.onTagAddError = _onTagAddError;
    vm.onDeleteTag = _onDeleteTag;
    vm.OnTagError = _OnTagError;

    vm.wikiPage = null;
    vm.wikiPageId = null;
    vm.title = 'Add/Create Wiki Page';

    vm.language = [{ id: 1, name: 'C#' }, { id: 2, name: 'C++' }, { id: 3, name: 'Java' }];


    _init();

    function _init() {
        console.log("manage init -> route params", vm.$routeParams);
        vm.$wikiService.getAllSpaces(vm.onReceiveSpaces, vm.onSpaceError);
        vm.$wikiService.getTags(_onTagSuccess, _onTagError);

        if (vm.$routeParams.wikiPageId) {
            //  edit mode - go look up record
            vm.title = 'Edit Wiki Page #' + vm.$routeParams.wikiPageId;
            vm.$wikiService.get(vm.$routeParams.wikiPageId, _onGetWikiSuccess, _onGetWikiError);
            vm.$wikiService.getTags(_onTagSuccess, _onTagError);            
        }
        else {
            //  create mode
            vm.title = 'Add New Wiki Page';

            vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
                title: "Add New <strong>Wiki Page</strong> ",
                subtitle: "Wiki Manager",
                buttons: [
                    { link: "#/", icon: "fa-arrow-left", text: "Back to List" }                    
                ]
            });
        }
    }
    function _submit() {
        console.log("data to save", vm.wikiPage);

        if (vm.$routeParams.wikiPageId) {
            vm.$wikiService.update(vm.$routeParams.wikiPageId, vm.wikiPage, vm.updateSuccess, vm.onUpdateError);
        }
        else {
            vm.$wikiService.add(vm.wikiPage, vm.addSuccess, vm.onAddError);
        }
    }

    function _createSlug(text)
    {
        if (!vm.wikiPage.url)
        {
            vm.wikiPage.url = vm.$utilityService.convertToSlug(text);
        }
                
        //vm.checkSlugUrl(vm.$utilityService.newSlug);
    }

    function _checkSlugUrl(slug)
    {
        var slug = vm.$utilityService.newSlug;
        vm.$wikiContentService.getPageBySlug(slug, vm.slugExists, vm.noSlug);
        vm.wikiPage.url = slug;
    }

    function _getWikiBySlugSuccess(response)
    {
        vm.$alertService.onError('There is already a slug with the name '+ response.item.url +', please choose a new slug...')
        
    }

    function _getWikiBySlugError(reponse)
    {
        vm.$alertService.onSuccess('There is no slug with that name, you may use that slug or choose a new one...')
        
    }

    function _onGetWikiSuccess(response) {

        var wiki = response.item;

        if (wiki.publishDate)
            wiki.publishDate = new Date(wiki.publishDate);

        if (wiki.wikiSpaces && wiki.wikiSpaces.length) {
            for (x = 0; x < wiki.wikiSpaces.length; x++)
                vm.chosenWikiSpaces.push(wiki.wikiSpaces[x].id)
        }


        console.log("chosen wiki spaces", vm.chosenWikiSpaces);

        vm.notify(function () {
            vm.wikiPage = wiki;
        });

        console.log("wiki from server", vm.wikiPage);

        if (vm.$routeParams.moduleId) {
        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
                title: "Edit Module <strong>" + vm.wikiPage.title + "</strong> ",
                subtitle: "Sections Manager",
                buttons: [
                    { link: "#/manage/" + vm.$routeParams.sectionId + "/modules", icon: "fa-arrow-left", text: "Back to Module" }
                    , { link: "#/manage/" + vm.$routeParams.sectionId + "/modules/" + vm.$routeParams.moduleId + "/wiki/edit/" + vm.wikiPage.id + "/content", icon: "fa-puzzle-piece", text: "Edit Module Wiki Content" }
                ]
            });
        }
        else {

            vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
            title: "Edit Wiki Page <strong>" + vm.wikiPage.title + "</strong> ",
            subtitle: "Wiki Manager",
            buttons: [
                { link: "#/", icon: "fa-arrow-left", text: "Back to List" }
                , { link: "#/edit/" + vm.wikiPage.id + "/content", icon: "fa-puzzle-piece", text: "Edit Page Content" }
            ]
        });
    }
    }

    function _onGetWikiError(response) {
        console.error("error getting wiki for edit", response);
    }

    function _updateSuccess(data) {
        console.log(data);
    }

    function _onUpdateError(jqXhr, error) {
        console.log(error);
    }

    function _onAddError(jqXhr, error) {
        console.log("error adding page", jqXhr);
    }

    function _addSuccess(data) {

        vm.wikiPageId = data.item;

        if (vm.$routeParams.moduleId) {
            console.log("page added, now add page to module: %s -> %s", vm.wikiPageId, vm.$routeParams.moduleId);

            vm.$modulesService.addWikiPage(vm.$routeParams.moduleId, vm.wikiPageId, _onAddModulePageSuccess, _onAddModulePageError);
        }
        else {

            vm.$alertService.onSuccess("Wiki page added: " + vm.wikiPageId)

            var url = "edit/" + vm.wikiPageId + "/content";

            console.log("go to url", url);

            vm.notify(function () {
                vm.$location.url(url);
            });
        }
    }

    function _onAddModulePageSuccess(data) {
        console.log("module page added", data);

        vm.$alertService.onSuccess("Module page added.")

        vm.notify(function () {
            vm.$location.url("/manage/" + vm.$routeParams.sectionId + "/modules/" + vm.$routeParams.moduleId + "/wiki/edit/" + vm.wikiPageId + "/content")
        });
    }

    function _onAddModulePageError(response) {

        vm.$alertService.onError("There was a problem attaching the page to the module.");

        console.log("error adding module page", response);
    }

    function _onReceiveSpaces(data) {
        vm.notify(function () {
            vm.spaceList = data.items;
        });
    }

    function _addSpaces(data) {
        console.log(data);
    }

    function _onSpaceError(jqXhr, error) {
        console.log(error);
    }

    function _checkboxClicked($event, spaceId) {
        console.log($event);
        console.log("checkbox clicked: %s -> checked is %s\n(note: if checked is undefined then none of the ajax calls will work right now)", spaceId);
        var indexof = vm.chosenWikiSpaces.indexOf(spaceId);

        if (indexof > -1) {
            vm.chosenWikiSpaces.splice(indexof, 1);
            vm.$wikiService.deleteSpace(vm.$routeParams.wikiPageId, spaceId, vm.onDeleteSpace, vm.onSpaceError);

            console.log("it's in the arrray!")
        }
        else {
            vm.chosenWikiSpaces.push(spaceId);
            vm.$wikiService.updateSpaceId(vm.$routeParams.wikiPageId, spaceId, vm.onSpaceUpdateSuccess, vm.onSpaceError);

            console.log("it's NOT in the array!");
        }
        }

    vm.updateSelection = function (checkbox, clicked) {
        var checkbox = $event.target;
        var clicked = (checkbox.checked);
        vm.checkboxClicked($event, spaceId);
    };

    function _onSpaceUpdateSuccess(data) {

        vm.$alertService.onSuccess("The space was added.");

        console.log(data);
    }
    function _onDeleteSpace(data) {

        vm.$alertService.onSuccess("The space was removed.");

        console.log(data);
    }

 
    function _tagAdded(tag) {
       
        vm.$wikiService.addTag(vm.$routeParams.wikiPageId, tag.id, vm.OnTagAddSuccess, vm.onTagAddError);
    }

    function _tagRemoved(tag) {
        
        vm.$wikiService.deleteTag(vm.$routeParams.wikiPageId, vm.onDeleteTag, vm.OnTagError);
    }

    function _onTagSuccess(response) {
        
        vm.notify(function () {
          vm.tagList = response.items;
            });
    }
    function _onTagError(jqXhr, error) {
        console.log(error);
    }

    function _OnTagAddSuccess(data) {
        console.log(data);
    }
    function _onTagAddError(jqXhr, error) {
        console.log(error);
    }

    function _onDeleteTag(data) {
        console.log(data);
    }

    function _OnTagError(jqXhr, error) {
        console.log(error);
    }
}


sabio.ng.addController(sabio.ng.app.module
, "adminWikiManageController"
, ['$scope', '$baseController', '$wikiService', '$routeParams', '$modulesService', '$utilityService', '$wikiContentService', '$alertService']
, sabio.page.adminWikiManageControllerFactory);