sabio.page.adminSectionsEnrollmentControllerFactory = function (
    $scope
    , $baseController
    , $sectionsService
    , $userDataService
    , $utilityService
    , $routeParams
    , $location
    , $route
    , $templateCache
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$sectionsService = $sectionsService;
    vm.$userDataService = $userDataService;
    vm.$utilityService = $utilityService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;

    vm.notify = vm.$sectionsService.getNotifier($scope);


    vm.settings = _settings;
    vm.getOnboardedStudents = _getOnboardedStudents;
    vm.getSectionStudents = _getSectionStudents;
    vm.date = _date;
    vm.dragoverCallback = _dragoverCallback;
    vm.dropCallback = _dropCallback;
    vm.logListEvent = _logListEvent;

    vm.onGetSectionStudentSuccess = _onGetSectionStudentSuccess;
    vm.onGetUserInSectionError = _onGetUserInSectionError;
    vm.onGetOnboardedStudentsSuccess = _onGetOnboardedStudentsSuccess;
    vm.onGetStudentsByRoleError = _onGetStudentsByRoleError;

    vm.onUpdateUserError = _onUpdateUserError;
    vm.onUpdateUserSuccess = _onUpdateUserSuccess;
    vm.onDeleteUserSuccess = _onDeleteUserSuccess;
    vm.onDeleteUserError = _onDeleteUserError;
    vm.reloadTemplate = _reloadTemplate;
    vm.addSectionStudent = _addSectionStudent;
    vm.deleteSectionUser = _deleteSectionUser;

    vm.sections = {};

    _init();

    function _init() {
        vm.settings();
        _getSectionStudents();
    }

    function _settings() {
        vm.user = {
            selected: null,
            lists: { "Section": [], "All": [] }
        };
    }

    function _getSectionStudents() {
        vm.$sectionsService.getUsersInSection(vm.$routeParams.sectionId, _onGetSectionStudentSuccess, _onGetUserInSectionError);
    }

    function _getOnboardedStudents() {
        vm.$userDataService.getOnboardedStudents(vm.onGetOnboardedStudentsSuccess, _onGetStudentsByRoleError);
    }

    function _onGetSectionStudentSuccess(response) {
        
        var sectionUser = response.items;

        if (response.items == null) {
            _getOnboardedStudents();
        } else {
            for (var a = 0; a < sectionUser.length; a++) {

                var UserFirstName = sectionUser[a].firstName;
                var UserLastName = sectionUser[a].lastName;
                var UserEmail = sectionUser[a].email;
                var Uid = sectionUser[a].userId;
                vm.user.lists.Section.push({ firstName: UserFirstName, lastName: UserLastName, Uid: Uid, chosen: true });
            }
            _getOnboardedStudents();
        }
    }

    function _onGetUserInSectionError(response) {
        console.error("error getting user", response);
    }

    function _onGetOnboardedStudentsSuccess(response) {

        vm.notify(function () {
            var onboardedStudent = response.items;

            for (x = 0; x < onboardedStudent.length; x++) {

                var isChosen = false;
                for (i = 0; i < vm.user.lists.Section.length; i++) {
                    if (onboardedStudent[x].userId === vm.user.lists.Section[i].Uid) {
                        isChosen = true;
                        break;
                    }
                }

                if (isChosen === false) {
                    var date = vm.date(onboardedStudent[x].desiredSabioStartDate);
                    vm.user.lists.All.push({
                        firstName: onboardedStudent[x].firstName,
                        lastName: onboardedStudent[x].lastName,
                        Uid: onboardedStudent[x].userId,
                        desiredTrack: onboardedStudent[x].track.name,
                        desiredCampusLocation: onboardedStudent[x].campuses.name,
                        desiredSabioStartDate: onboardedStudent[x].desiredSabioStartDate,
                        chosen: false
                    });
                }
            }

            //var g = "songokou";
            //var l = g.length;
            
            //for (var x = 0 ; x <= l; x++) {
            //    for (var i = l; i >= x ; i--) {
            //        var s = g.substring(x, l);
            //        console.log(s);
            //    }
            //}

        });
    }

    function _date(date) {
        if (date)
            date = vm.$utilityService.convertStringDateToUTC(date);
        return date;
    }

    function _onGetStudentsByRoleError(response) {
        console.error("error getting students", response);
    }

    function _dragoverCallback(event, index, external, type) {
        vm.logListEvent('dragged over', event, index, external, type);
        return index < 10;
    };

    function _dropCallback(event, index, item, external, type, allowedType) {
        vm.logListEvent('dropped at', event, index, external, type);
        if (type == 'Chosen') {
            _addSectionStudent(item);
        }
        else {
            _deleteSectionUser(item);
        }
        return item;
    };

    function _addSectionStudent(item) {
        vm.$sectionsService.updateUser(item.Uid, vm.$routeParams.sectionId, vm.onUpdateUserSuccess, vm.onUpdateUserError);
        vm.reloadTemplate();
    }

    function _deleteSectionUser(item) {
        vm.$sectionsService.deleteUser(item.Uid, vm.$routeParams.sectionId, vm.onDeleteUserSuccess, vm.onDeleteUserError);
        vm.reloadTemplate();
    }

    function _onUpdateUserError(response) {
        console.log(response);
    }

    function _onUpdateUserSuccess(response) {
        console.log(response);
    }

    function _onDeleteUserSuccess(response) {
        console.log(response);
    }

    function _onDeleteUserError(response) {
        console.log(response);
    }

    function _logListEvent(action, event, index, external, type) {
        var message = external ? 'External ' : '';
        message += type + ' element is ' + action + ' position ' + index;
    };

    function _reloadTemplate() {
        var currentPageTemplate = vm.$route.current.templateUrl;
        vm.$templateCache.remove(currentPageTemplate);
        vm.$route.reload();
    }
}


sabio.ng.addController(sabio.ng.app.module
, "adminSectionsEnrollmentController"
, ['$scope', '$baseController', '$sectionsService', '$userDataService', '$utilityService', '$routeParams', '$location', '$route', '$templateCache']
, sabio.page.adminSectionsEnrollmentControllerFactory);