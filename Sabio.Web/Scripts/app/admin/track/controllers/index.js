sabio.page.adminTrackIndexControllerFactory = function (
    $scope
    , $baseController
    , $trackService
    , $routeParams
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$trackService = $trackService;
    vm.notify = vm.$trackService.getNotifier($scope);

    vm.delete = _delete;
    vm.getCourse = _getCourse;
    vm.tracks = null;
    vm.trackType = 1;
    vm.totalCourses = [];
    vm.courses = null;
    vm.ckeditorOptions = {
        language: 'en',
        allowedContent: true,
        entities: false
    };

    _init();

    function _init() {
        vm.$trackService.getTracks(_onGetTracksSuccess, _onGetTracksError);
    }

    function _delete(pageId) {
        alert("TODO: go delete page #" + pageId);
    }

    function _onGetTracksSuccess(response) {
        vm.notify(function () {
            vm.tracks = response.items;
        });
       // console.log("tracks from server", vm.tracks);
    }

    function _onGetTracksError(response) {

    }

    function _getCourse(id) {

        vm.$trackService.getCourseById(id, _getCourseSuccess, _getCourseError);

    }

    function _getCourseSuccess(response) {
        vm.notify(function () {
            vm.course = response.item;
        });
        //console.log(vm.course)
        vm.$trackService.getSectionByCourseId(vm.course.id, _getSectionSuccess, _getSectionError)

    }

    function _getCourseError(error) {
        console.error(error)

    }

    function _getSectionSuccess(response) {
        vm.notify(function () {
            vm.section = response.item;
        })
        //console.log(vm.section);
    }

    function _getSectionError(error) {
        console.error(error);
    }




}

sabio.ng.addController(sabio.ng.app.module
, "adminTrackIndexController"
, ['$scope', '$baseController', '$trackService', '$routeParams']
, sabio.page.adminTrackIndexControllerFactory);