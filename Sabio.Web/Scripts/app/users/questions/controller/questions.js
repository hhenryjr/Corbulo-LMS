sabio.page.questionsControllerFactory = function (
    $scope
    , $baseController
    , $officeHoursService
    , $routeParams
    , $alertService
    ) {

    var vm = this;
    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$officeHoursService = $officeHoursService;
    vm.$routeParams = $routeParams;
   
    vm.notify = vm.$officeHoursService.getNotifier($scope);


    vm.title = "angular is good to go!"
    vm.officeHourId = null;
    vm.onSuccessById = _onSuccessById;
    vm.onErrorById = _onErrorById;
    vm.animationsEnabled = true;
    _init();

    function _init() {
        vm.officeHourId = vm.$routeParams.officeHourId;
        vm.$officeHoursService.getByOfficeHourId(vm.officeHourId, _onSuccessById, _onErrorById);
    }

    function _onSuccessById(response) {
       
        vm.questions = response.items;
        console.log(vm.questions);
    }
    function _onErrorById(response) {
        console.log(response);
    }

   

}

sabio.ng.addController(sabio.ng.app.module
, "QuestionsController"
, ['$scope', '$baseController', '$officeHoursService', '$routeParams', '$alertService']
, sabio.page.questionsControllerFactory);


