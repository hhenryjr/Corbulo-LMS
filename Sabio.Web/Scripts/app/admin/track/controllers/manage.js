sabio.page.adminTrackManageControllerFactory = function (
    $scope
    , $baseController
    , $trackService
    , $routeParams
    , $location
    , $route
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$trackService = $trackService;
    vm.$routeParams = $routeParams;
    vm.notify = vm.$trackService.getNotifier($scope);
    vm.updateSuccess = _updateSuccess;
    vm.onUpdateError = _onUpdateError;
    vm.addSuccess = _addSuccess;
    vm.onAddError = _onAddError;
    vm.submit = _submit;
    vm.isChecked = _isChecked;
    vm.chosenCourses = [];

    vm.format = [{ id: 1, format: 'In Person-Training' },
                   { id: 2, format: 'Online Training' }]

    //vm.onReceiveTrack = _onReceiveTrack;
    //vm.onTrackError = _onTrackError;
    vm.courses = null;
    vm.trackCourses = null;
    vm.singleTrack = null;
    vm.title = 'Edit/Remove Track Page';
    vm.ckeditorOptions = {
        language: 'en',
        allowedContent: true,
        entities: false
    };


    //vm.$trackService.getTracks(vm.onReceiveTrack, vm.onTrackError);
    _init();

    function _init() {
        console.log("manage init -> route params", vm.$routeParams);
        vm.$trackService.getAllCourses(_onGetCoursesSuccess, _onGetCoursesError);
        //vm.$trackService.getTracks(_onGetTracksSuccess, _onGetTracksError);
        if (vm.$routeParams.trackId) {
            //  edit mode - go look up record
            vm.title = 'Edit Track Page #' + vm.$routeParams.trackId;
            vm.$trackService.get(vm.$routeParams.trackId, _onGetTrackSuccess, _onGetTrackError);
            
        }
        else {
            //  create mode
            vm.title = 'Add New Track';

        }
        
    }

    function _submit() {
        console.log("data to save", vm.singleTrack);
        //vm.singleTrack.courseIds
        for (i = 0; i < vm.chosenCourses.length; i++)
        {
            vm.singleTrack.courseIds = vm.chosenCourses;
        }
        console.log("data to save", vm.singleTrack);
        vm.showNewTrackError = true;
        if (vm.$routeParams.trackId) {
            console.log(vm.singleTrack + vm.$routeParams.trackId);
            vm.$trackService.update(vm.$routeParams.trackId, vm.singleTrack, vm.updateSuccess, vm.onUpdateError);
            
        }
        else {
            //alert("TODO: execute POST for new wikiPage");
            vm.$trackService.add(vm.singleTrack, vm.addSuccess, vm.onAddError);
            console.log(vm.singleTrack);
        }
    }

    function _isChecked($event, id) {
        
        var index = vm.chosenCourses.indexOf(id);

        if (index > -1) {
            vm.chosenCourses.splice(index, 1);
        } else {
            vm.chosenCourses.push(id);
        }
       
    }

    function _onGetTrackSuccess(response) {

        var track = response.item;
        

        vm.notify(function () {

            vm.singleTrack = track;
            vm.trackFormat = vm.singleTrack.format;
            //vm.trackDescription = vm.singleTrack.description
            
        });
        //vm.isChecked(vm.singleTrack.courseIds, vm.courses);
        console.log("track from server", vm.singleTrack);

        if (vm.singleTrack.tracksCourses && vm.singleTrack.tracksCourses.length) {
            for (i = 0; i < vm.singleTrack.tracksCourses.length; i++) {
                vm.chosenCourses.push(vm.singleTrack.tracksCourses[i].courseId);
            }
        }
        
    }

    function _onGetTrackError(response) {
        console.error("error getting track for edit", response);
    }


    function _updateSuccess(data) {
        console.log(data);
    }

    function _onUpdateError(jqXhr, error) {
        console.log(error);
    }

    function _onAddError(jqXhr, error) {
        console.log(error);
    }

    function _addSuccess(data) {
        console.log(data);
        //vm.$trackService.getTracks(_onGetTracksSuccess, _onGetTracksError);
        $location.path('/');
        $route.reload(true);
    }

    function _onGetCoursesSuccess(response) {
        vm.notify(function () {
            vm.courses = response.items;
        })
        

        console.log(vm.courses);
    }

    function _onGetCoursesError(error) {
        console.error(error);
    }
}

sabio.ng.addController(sabio.ng.app.module
, "adminTrackManageController"
, ['$scope', '$baseController', '$trackService', '$routeParams', '$location', '$route']
, sabio.page.adminTrackManageControllerFactory);