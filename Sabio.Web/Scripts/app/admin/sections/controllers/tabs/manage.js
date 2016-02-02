sabio.page.adminSectionsManageControllerFactory = function (
    $scope
    , $routeParams
    , $location
    , $daysEnum
    , $baseController
    , $sectionsService
    , $utilityService
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$daysEnum = $daysEnum;
    vm.$sectionsService = $sectionsService;
    vm.$utilityService = $utilityService;
    vm.notify = vm.$sectionsService.getNotifier($scope);
    
    vm.loadSection = _loadSection;
    vm.submitBasic = _submitBasic;
    vm.datepickerDisabled = _datepickerDisabled;
    vm.openDatepicker = _openDatepicker;

    vm.chosenDays = null;
    vm.section = { daysOfWeek: 0 };
    vm.courses = null;
    vm.courseFormats = null;
    vm.enrollStatus = null;
    vm.timezones = null;
    vm.showSectionFormErrors = false;
    vm.datepicker = {
        format: 'dd-MMMM-yyyy'
        , status: {
            start: false,
            end: false,
            reg: false
        }
        , options: {
            formatYear: 'yy',
            startingDay: 1
        }
        , defaults: {
            start: { min: new Date(), max: new Date(2020, 5, 22) }
            , end: { min: new Date(), max: new Date(2020, 5, 22) }
            , reg: { min: new Date(), max: new Date(2020, 5, 22) }
        }
    };

    _init();

    function _init() {
        var cf = JSON.parse($("#courseFormats").html());
        vm.courseFormats = [];
        angular.forEach(cf, function (val, key) {
            vm.courseFormats.push({
                id: key,
                value: val
            })
        });

        var tz = JSON.parse($("#timezones").html());
        vm.timezones = [];
        angular.forEach(tz, function (val, key) {
            vm.timezones.push({
                id: key,
                value: val
            })
        });

        var es = JSON.parse($("#enrollmentStatus").html());
        vm.enrollStatus = [];
        angular.forEach(es, function (val, key) {
            vm.enrollStatus.push({
                id: key,
                value: val
            })
        });

        _loadCourses();

        if (!vm.$routeParams.sectionId)
        {
            vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
                title: "Create New <strong>Section</strong>",
                subtitle: "Sections Manager",
                buttons: [
                    { link: "#/", icon: "fa-arrow-left", text:"Back to List" }
                ]
            });
        }
      
    }

    function _submitBasic() {
        var data = vm.section;

        data = _processSectionData(data);

        if (vm.section.id) {
            //console.log("UPDATE data", data);
            vm.$sectionsService.updateSection(vm.section.id, data, _onUpdateSectionSuccess, _onSectionError);
        }
        else {
            //console.log("INSERT data", data);
            vm.$sectionsService.addSection(data, _onCreateSectionSuccess, _onSectionError);
        }
    }

    function _openDatepicker($event, type) {
        angular.forEach(vm.datepicker.status, function (val, key) {
            vm.datepicker.status[key] = false;
        });

        vm.datepicker.status[type] = true;
    }

    function _datepickerDisabled(date, mode) {
        return false; //( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
    }

    function _loadCourses() {
        vm.$sectionsService.getCourses(_onGetCoursesSuccess, _onGetCoursesError);
    }

    function _loadSection() {
        if (vm.$routeParams.sectionId)
            vm.$sectionsService.getSection(vm.$routeParams.sectionId, _onGetSectionSuccess, _onGetSectionError);
    }   

    function _onGetSectionSuccess(response) {

        var section = _initSectionData(response.item);

        vm.notify(function () {
            vm.section = section;
        });

        vm.$eventHandlerService.broadcast(vm.$eventsEnum.HEADER_DATA, {
            title: "Section #" + section.id + " <strong>(" + section.course.courseName +") </strong>",                        
            subtitle: "Sections Manager",
            buttons: [
                { link: "#/", icon: "fa-arrow-left", text: "Back to List" }
            ]
        });
    }

    function _onGetSectionError(response) {
        console.error("error getting section", response);
    }

    function _onGetCoursesSuccess(response) {
        vm.courses = response.items;
        _loadSection();
    }

    function _onGetCoursesError(response) {
        console.error("error getting courses", response);
    }

    function _onUpdateSectionSuccess(response) {

        _loadSection();
    }

    function _onCreateSectionSuccess(response) {

        vm.$location.url("manage/" + response.item);
    }

    function _onSectionError(response) {
        console.error("section error", response);
    }

    function _processSectionData(section) {
        //console.log('process section', section);
        if (section.course)
            delete section.course;

        if (section.campus)
            delete section.campus;

        //if (section.instructors)
        //    delete section.instructors;

        section.courseId = section.courseId.id;
        section.format = section.format.id;
        section.timeZone = section.timeZone.id;

        section.startDate = vm.$utilityService.serializeDatepicker(section.startDate);
        section.endDate = vm.$utilityService.serializeDatepicker(section.endDate);
        section.registrationDeadline = vm.$utilityService.serializeDatepicker(section.registrationDeadline);

        section.startTime = vm.$utilityService.militaryTimeFromDate(section.startTime);
        section.endTime = vm.$utilityService.militaryTimeFromDate(section.endTime);

        return section;
    }

    function _initSectionData(section) {
        for (x = 0; x <= vm.courses.length; x++) {
            if (section.courseId == vm.courses[x].id) {
                section.courseId = vm.courses[x];
                break;
            }
        }

        for (x = 0; x <= vm.courseFormats.length; x++) {
            if (section.format == vm.courseFormats[x].id) {
                section.format = vm.courseFormats[x];
                break;
            }
        }

        for (x = 0; x <= vm.timezones.length; x++) {
            if (section.timeZone == vm.timezones[x].id) {
                section.timeZone = vm.timezones[x];
                break;
            }
        }
        vm.chosenDays = section.daysOfWeek;

        section.startTime = vm.$utilityService.dateFromMilitaryTime(section.startTime);
        section.endTime = vm.$utilityService.dateFromMilitaryTime(section.endTime);

        if (section.startDate)
            section.startDate = vm.$utilityService.convertStringDateToUTC(section.startDate);

        if (section.endDate)
            section.endDate = vm.$utilityService.convertStringDateToUTC(section.endDate);

        if (section.registrationDeadline)
            section.registrationDeadline = vm.$utilityService.convertStringDateToUTC(section.registrationDeadline);

        return section;
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminSectionsManageController"
, ['$scope', '$routeParams', '$location', '$daysEnum', '$baseController', '$sectionsService','$utilityService']
, sabio.page.adminSectionsManageControllerFactory);