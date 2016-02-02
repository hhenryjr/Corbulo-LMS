sabio.page.adminSectionsInstructorsControllerFactory = function (
    $scope
    , $baseController
    , $sectionsService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$sectionsService = $sectionsService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;

    vm.settings = _settings;

    vm.onGetInstructorsSuccess = _onGetInstructorsSuccess;
    vm.onGetInstructorsError = _onGetInstructorsError;

    vm.onInstructorDrop = _onInstructorDrop;
    vm.onInstructorDragover = _onInstructorDragover;

    vm.onAddInstructorsSuccess = _onAddInstructorsSuccess;
    vm.onAddInstructorsError = _onAddInstructorsError;

    vm.deleteSectionInstructorSuccess = _deleteSectionInstructorSuccess;

    vm.getChosenInstructorsSuccess = _getChosenInstructorsSuccess;
    vm.getChosenInstructorsError = _getChosenInstructorsError;

    vm.reloadTemplate = _reloadTemplate;

    vm.notify = vm.$sectionsService.getNotifier($scope);

    _invokeItemsOnTemplateLoad();

    function _invokeItemsOnTemplateLoad() {
        vm.settings();
        vm.$sectionsService.getChosenInstructors(vm.$routeParams.sectionId, _getChosenInstructorsSuccess, _getChosenInstructorsError);
    }

    function _settings() {
        vm.sections = null;
        vm.instructors = {
            selected: null,
            lists: { "Chosen": [], "List_Of": [] }
        };
    }

    function _addSectionInstructor(item) {
        vm.$sectionsService.addSectionInstructors(item.id, vm.$routeParams.sectionId, _onAddInstructorsSuccess(item.id), _onAddInstructorsError);
        vm.reloadTemplate();
    }

    function _deleteSectionInstructor(item) {
        vm.$sectionsService.deleteSectionInstructor(item.id, vm.$routeParams.sectionId, _deleteSectionInstructorSuccess(item.id), _deleteSectionInstructorError);
        vm.reloadTemplate();
    }

    function _getChosenInstructorsSuccess(data) {
        var inst = data.items;

        if (data.items == null) {
            vm.$sectionsService.getInstructors(_onGetInstructorsSuccess, _onGetInstructorsError);
        } else {
            for (x = 0; x < inst.length; x++) {
                var instId = inst[x].instructorId;
                var instName = inst[x].name;
                vm.instructors.lists.Chosen.push({ name: instName, id: instId, chosen: true });
            }
            vm.$sectionsService.getInstructors(_onGetInstructorsSuccess, _onGetInstructorsError);
        }

    }

    function _onGetInstructorsSuccess(response) {

        vm.notify(function () {
            var inst = response.items;
            for (x = 0; x < inst.length; x++) {
                var isChosen = false;
                for (n = 0; n < vm.instructors.lists.Chosen.length; n++) {
                    if (inst[x].id === vm.instructors.lists.Chosen[n].id) {
                        isChosen = true;
                        break;
                    }
                }
                if (isChosen === false) {
                    vm.instructors.lists.List_Of.push({ name: inst[x].name, id: inst[x].id, chosen: false });
                }
            }
        });

    }

    function _deleteSectionInstructorSuccess(id, response) {
        console.log("Instructor with Id#" + " " + id + " " + "was deleted from the Section Instructors List");
    }

    function _onAddInstructorsSuccess(id, response) {
        console.log("Instructor with Id#" + " " + id + " " + "was added to the Section Instructors List");
    }

    function _onAddInstructorsError(response) {
        console.log(response);
    }

    function _getChosenInstructorsError(response) {
        console.log(response);
    }

    function _onGetInstructorsError(response) {
        console.log(response);
    }

    function _deleteSectionInstructorError(response) {
        console.log(response);
    }

    function _onInstructorDragover(event, index, external, type) {
        return index < 10;
    };

    function _onInstructorDrop(event, index, item, external, type, allowedType) {
        if (type == 'Chosen') {
            _addSectionInstructor(item);
        }
        else {
            _deleteSectionInstructor(item);
        }
        return item;
    };

    function _reloadTemplate() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsInstructorsController"
, ['$scope', '$baseController', '$sectionsService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.adminSectionsInstructorsControllerFactory);

