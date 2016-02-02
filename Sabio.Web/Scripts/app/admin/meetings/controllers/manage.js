sabio.page.adminMeetingsManageControllerFactory = function (
    $scope
    , $routeParams
    , $location
    , $baseController
    , $meetingsService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    // Set up controller infrastructure
    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$meetingsService = $meetingsService;
    vm.notify = vm.$meetingsService.getNotifier($scope);

    // Model Properties
    vm.meeting = null;  // This is the main object for the controller to edit

    // View Model properties for controlling state of View
    vm.showFormErrors = false;

    // Expose methods that view needs to access
    vm.loadMeeting = _loadMeeting;
    vm.submitBasic = _submitBasic;

    // This is the "Fold". Logic above here comprises the 
    // publicly exposed controller functionality. Logic
    // below here is intended to be private to the controller
    // implementation

    _init(); // Executes the controller initialization

    // Logic to initialize the controller itself E.g.:
    // 1. Populate model lists from enums, services, etc.
    // 2. Set default state of UI visibility, etc.
    // 3. Read route params to set up controller.
    function _init() {
        //vm.currentTab = vm.tabs[0];

        //var cf = JSON.parse($("#courseFormats").html());
        //vm.courseFormats = [];
        //angular.forEach(cf, function (val, key) {
        //    vm.courseFormats.push({
        //        id: key,
        //        value: val
        //    })
        //});

        //if (!vm.$routeParams.sectionId)
        //    vm.tabs = [vm.tabs[0]]; //  only show the first tab if we are in create mode

        _loadMeeting();
    }

    function _submitBasic() {
        var data = vm.meeting;

        data = _processMeetingData(data);

        if (vm.meeting.id) {
            console.log("UPDATE data", data);
            vm.$meetingsService.update(vm.meeting.id, data, _onUpdateSuccess, _onUpsertError);
        }
        else {
            console.log("INSERT data", data);
            vm.$meetingsService.add(data, _onInsertSuccess, _onUpsertError);
        }
    }

    function _loadMeeting() {
        if (vm.$routeParams.meetingId)
            vm.$meetingsService.getById(vm.$routeParams.meetingId, _onGetSuccess, _onGetError);
    }

    function _onGetSuccess(response) {
        var meeting = _initMeetingData(response.item);
        vm.notify(function () {
            vm.meeting = meeting;
        });
    }

    function _onGetError(response) {
        console.error("Get meeting failed", response);
    }

    function _onUpdateSuccess(response) {
        _loadMeeting();
    }

    function _onInsertSuccess(response) {

        vm.$location.url("manage/" + response.item);
    }

    function _onUpsertError(response) {
        console.error("Save error", response);
    }

    function _processMeetingData(meeting) {
        // Place to put logic to prepare object
        // for saving to database after editing

        //meeting.startDate = meeting.startDate.toLocaleDateString("en-US");
        //meeting.startTime = _timeFromDate(meeting.startTime);
        //meeting.endTime = _timeFromDate(meeting.endTime);

        console.log('process meeting', meeting);
        return meeting;
    }

    function _initMeetingData(meeting) {
        // Place to put logic to prepare object for
        // editing after it is fetched from database.

        //for (x = 0; x <= vm.courses.length; x++) {
        //    if (section.courseId == vm.courses[x].id) {
        //        section.courseId = vm.courses[x];
        //        break;
        //    }
        //}

        //section.startTime = _dateFromTime(section.startTime);
        //section.endTime = _dateFromTime(section.endTime);

        //if (section.startDate)
        //    section.startDate = new Date(section.startDate);

        //if (section.endDate)
        //    section.endDate = new Date(section.endDate);

        //if (section.registrationDeadline)
        //    section.registrationDeadline = new Date(section.registrationDeadline);

        console.log("Initializing meeting data", section);

        return meeting;
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminMeetingsManageController"
, ['$scope', '$routeParams', '$location', '$baseController', '$meetingsService']
, sabio.page.adminMeetingsManageControllerFactory);