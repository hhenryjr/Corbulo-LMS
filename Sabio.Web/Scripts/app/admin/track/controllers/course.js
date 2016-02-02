sabio.page.adminTrackContentControllerFactory = function (
      $scope
    , $baseController
    , $trackService
    , $wikiService
    , $coursesService
    , $routeParams
    , $location
    , $route
    , $templateCache
    , $utilityService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;

    vm.$trackService = $trackService;
    vm.$wikiService = $wikiService;
    vm.$coursesService = $coursesService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$route = $route;
    vm.$templateCache = $templateCache;
    vm.$utilityService = $utilityService;

    vm.processCourseData = _processCourseData;
    vm.updateCourseDetails = _updateCourseDetails;
    vm.onUpdateCourseSuccess = _onUpdateCourseSuccess;
    vm.onUpdateCourseError = _onUpdateCourseError;
  
    vm.getChosenInstructors = _getChosenInstructors;

    vm.onTagSuccess = _onTagSuccess;
    vm.onTagError = _onTagError;

    vm.onGetCourseSuccess = _onGetCourseSuccess;
    vm.onGetCourseError = _onGetCourseError;

    vm.tagAdded = _tagAdded;
    vm.tagRemoved = _tagRemoved;

    vm.prereqAdded = _prereqAdded;
    vm.prereqRemoved = _prereqRemoved;

    vm.onPrereqsError = _onPrereqsError;
    vm.onPrereqsSuccess = _onPrereqsSuccess;

    vm.onPrereqAddSuccess = _onPrereqAddSuccess;
    vm.onPrereqAddError = _onPrereqAddError;

    vm.onDeletePrereqSuccess = _onDeletePrereqSuccess;
    vm.onDeletePrereqError = _onDeletePrereqError;

    vm.onGetInstructorsSuccess = _onGetInstructorsSuccess;
    vm.onGetInstructorsError = _onGetInstructorsError;

    vm.onTagAddSuccess = _onTagAddSuccess;
    vm.onTagAddError = _onTagAddError;

    vm.onDeleteTag = _onDeleteTag;
    vm.onTagError = _onTagError;

    vm.onInstructorDrop = _onInstructorDrop;
    vm.onInstructorDragover = _onInstructorDragover;

    vm.onAddInstructorsSuccess = _onAddInstructorsSuccess;
    vm.onAddInstructorsError = _onAddInstructorsError;

    vm.deleteCourseInstructor = _deleteCourseInstructor;
    vm.deleteCourseInstructorSuccess = _deleteCourseInstructorSuccess;

    vm.getChosenInstructorsError = _getChosenInstructorsError;

    vm.notify = vm.$trackService.getNotifier($scope);
   
    _init();

    function _init() {
        settings();

        var cf = JSON.parse($("#courseFormats").html());
        vm.courseFormats = [];
        angular.forEach(cf, function (val, key) {
            vm.courseFormats.push({
                id: key,
                value: val
            })
        });
        
        vm.$wikiService.getTags(_onTagSuccess, _onTagError);
        vm.$coursesService.getAllPreq(_onPrereqsSuccess, _onPrereqsError);

        if (vm.$routeParams.courseId) {    
            vm.$trackService.getCourseById(vm.$routeParams.courseId, vm.onGetCourseSuccess, vm.onGetCourseError);
        }
    }

    
    
    function _updateCourseDetails(id) {

        vm.dates = {};

        vm.dates.start = vm.course.start;
        vm.dates.end = vm.course.end;

        var data = vm.dates;

        data = vm.processCourseData(data);

        vm.num_str = vm.course.format.id;
        vm.num_str = parseInt(vm.num_str, 10);
       
        var update = new Object();

        update.courseName = vm.course.courseName;
        update.length = vm.course.length;
        update.format = vm.num_str;
        update.description = vm.course.description;
        update.learningObjectives = vm.course.learningObjectives;
        update.expectedOutcome = vm.course.expectedOutcome;
        update.evaluationCriteria = vm.course.evaluationCriteria;
        update.start = vm.dates.start;
        update.end = vm.dates.end;

        vm.$coursesService.updateCourseDetails(id, update, vm.onUpdateCourseSuccess, vm.onUpdateCourseError);
    }

    function _onUpdateCourseSuccess(response) {
        console.log(response);
    }

    function _onTagSuccess(response) {
            vm.notify(function () {
                vm.tagList = response.items;
            });
    }

    function _tagAdded(tag) {    
        vm.$coursesService.addCourseTag(vm.$routeParams.courseId, tag.id, vm.onTagAddSuccess, vm.onTagAddError);
    }
    
    function _tagRemoved(tag) {
        vm.$coursesService.deleteCourseTag(vm.$routeParams.courseId, tag.tagId, vm.onDeleteTag, vm.onTagError);
    }

    function _prereqAdded(tag) {
        vm.$coursesService.addCoursePrereqs(vm.$routeParams.courseId, tag.id, vm.onPrereqAddSuccess, vm.onPrereqAddError);
    }

    function _prereqRemoved(tag) {
        vm.$coursesService.deleteCoursePrereqs(vm.$routeParams.courseId, tag.id, vm.onDeletePrereqSuccess, vm.onDeletePrereqError);
    }

    function _processCourseData(course) {
      
        course.start = vm.$utilityService.serializeDatepicker(course.start);
        course.end = vm.$utilityService.serializeDatepicker(course.end);
       
        return course;
    }

    function _initCourseData(course) {
        for (x = 0; x <= vm.courseFormats.length; x++) {
            if (course.format == vm.courseFormats[x].id) {
                course.format = vm.courseFormats[x];
                break;
            }
        }
        if (course.start)
            course.start = vm.$utilityService.convertStringDateToUTC(course.start);
        if (course.end)
            course.end = vm.$utilityService.convertStringDateToUTC(course.end);
        return course;
    }

    function _getChosenInstructors() {
        var inst = vm.course.instructors;
       
        if (inst == null ) {
            vm.$wikiService.getAllInstructors(_onGetInstructorsSuccess, _onGetInstructorsError);
        } else {
            for (x = 0; x < inst.length; x++) {
                var instId = inst[x].instructorId;
                var instName = inst[x].name;
                vm.instructors.lists.Chosen.push({ name: instName, id: instId, chosen: true });
            }
           vm.$wikiService.getAllInstructors(_onGetInstructorsSuccess, _onGetInstructorsError);
        }
    }

    function _addCourseInstructor(item) {
        vm.$wikiService.addCourseInstructor(vm.$routeParams.courseId, item.id, _onAddInstructorsSuccess(item.id), _onAddInstructorsError);
    }

    function _deleteCourseInstructor(item) {
        vm.$wikiService.deleteCourseInstructor(vm.$routeParams.courseId, item.id, _deleteCourseInstructorSuccess(item.id), _deleteCourseInstructorError);
    }

    function _onInstructorDragover(event, index, external, type) {
        return index < 10;
    };

    function _onInstructorDrop(event, index, item, external, type, allowedType) {
        if (type == 'Chosen') {
            _addCourseInstructor(item);
        }
        else {
            _deleteCourseInstructor(item);
        }
        return item;
    };


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

    function _onGetCourseSuccess(data) {
        var course = _initCourseData(data.item);
        vm.notify(function () {
            vm.course = course;
        });
        console.log(vm.course);
        _getChosenInstructors();
    }


    function _onPrereqsSuccess(response) {
        vm.notify(function () {
            vm.preqList = response.items;
        });
    }

    function _onPrereqAddSuccess(response) {
        console.log(response);
    }

    function _deleteCourseInstructorSuccess(id, response) {
        console.log("Instructor with Id#" + " " + id + " " + "was deleted from the Course Instructors List");
    }

    function _onAddInstructorsSuccess(id, response) {
        console.log("Instructor with Id#" + " " + id + " " + "was added to the Course Instructors List");
    }

    function _onDeleteTag(response) {
        console.log(response);
    }
    

    function _onTagAddSuccess(response) {
        console.log(response);
    }

    function _onDeletePrereqSuccess(response){
        console.log(response);
    }

    function _onDeletePrereqError(response){
        console.log(response);
    }

    function _onUpdateCourseError(response) {
        console.log(response);
    }

    function _onTagError() {
        console.log(response);
    }

    function _onTagAddError() {
        console.log(response);
    }

    function _onPrereqsError(response){
        console.log(response);
    }

    function _onPrereqAddError(response){
        console.log(response);
    }

    function _onGetCourseError(response) {
        console.log(response);
    }

    function _onTagError(response){
        console.log(response);
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

    function _deleteCourseInstructorError(response) {
        console.log(response);
    }

    function settings() {
      
        vm.format = [{ id: 1, format: 'In Person-Training' },
                        { id: 2, format: 'Online Training' }]

        vm.track = null;
        vm.trackContent = null;
        vm.selectedContent = null;
        vm.title = 'Add/Create Track';
        vm.pageContent = {};

        vm.ckeditorOptions = {
            language: 'en',
            allowedContent: true,
            entities: false
        };

        vm.tagList = [];

        vm.Courses = null;
        vm.instructors = {
            selected: null,
            lists: { "Chosen": [], "List_Of": [] }
        };
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminTrackContentController"
, ['$scope', '$baseController', '$trackService', '$wikiService', '$coursesService', '$routeParams', '$location', '$route', '$templateCache', '$utilityService']
, sabio.page.adminTrackContentControllerFactory);
