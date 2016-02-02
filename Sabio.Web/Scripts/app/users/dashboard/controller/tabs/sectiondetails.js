sabio.page.userControllerFactory = function (
      $scope
      , $baseController
      , $dashboardService
      , daysEnum
      , $utilityService
      ) {

    var vm = this;

    $baseController.merge(vm, $baseController, daysEnum);

    vm.$dashboardService = $dashboardService;
    vm.$scope = $scope;
    vm.daysEnum = daysEnum;
    vm.$utilityService = $utilityService;

    vm.courses = null;
    vm.courseFormats = null;
   
    vm.timezones = null;
  
    vm.initSectionData = _initSectionData;

    vm.receiveSectionDetails = _receiveSectionDetails;
    vm.onReceiveSectionDetailsError = _onReceiveSectionDetailsError;
    vm.receiveSectionInstructor = _receiveSectionInstructor;
    vm.onReceiveSectionInstructorError = _onReceiveSectionInstructorError;
    

    vm.notify = vm.$dashboardService.getNotifier($scope);

    render();

    function render() {
        settings();

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

        vm.$dashboardService.GetSectionDetailsByUserId(vm.receiveSectionDetails, vm.onReceiveSectionDetailsError);
    }


    function _receiveSectionDetails(data) {
        console.log(data);
        var section = _initSectionData(data.item);
        vm.notify(function () {
            vm.section = section;  
            angular.element("#enumDays").attr('readonly', true)
        });

        vm.$dashboardService.GetSectionInstructors(vm.section.id, vm.receiveSectionInstructor, vm.onReceiveSectionInstructorError);
    }

    function _initSectionData(section) {
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

    function _onReceiveSectionDetailsError(response) {
        console.log(response)
    }

    function _receiveSectionInstructor(data) {
        vm.notify(function () {
            vm.instructors = data.items;
        });
    }

    function _onReceiveSectionInstructorError(response) {
        console.log(response);
    }


    function settings() {
        vm.accordionOpen = true;
        vm.accordionDisabled = true;         
    }

}


sabio.ng.app.module.factory('daysEnum', [
   function () {
       return Object.freeze({
           SUNDAY: 1,
           MONDAY: 2,
           TUESDAY: 4,
           WEDNESDAY: 8,
           THURSDAY: 16,
           FRIDAY: 32,
           SATURDAY: 64
       });
   }
]);

sabio.ng.addController(sabio.ng.app.module
    , "userSectionDetailsController"
    , ['$scope', '$baseController', "$dashboardService", 'daysEnum', '$utilityService']
    , sabio.page.userControllerFactory);
