

sabio.page.adminAssignSectionsControllerFactory = function ($scope, $baseController, $userSectionsService, $routeParams, $location) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$userSectionsService = $userSectionsService;
    vm.$routeParams = $routeParams;
    vm.$location = $location;

    vm.getSectionsSuccess = _getSectionsSuccess;
    vm.getSectionsError = _getSectionsError;

    vm.getUserSuccess = _getUserSuccess;
    vm.getUserError = _getUserError;

    vm.assignSections = _assignSections;
    vm.updateStatusSuccess = _updateStatusSuccess;
    vm.updateStatusError = _updateStatusError;

    vm.backToList = _backToList;

    vm.notify = vm.$userSectionsService.getNotifier($scope);
   
    render();

    function render() {    
        vm.$userSectionsService.getSections(vm.getSectionsSuccess, vm.getSectionsError);
        vm.$userSectionsService.getUsersDetail(vm.$routeParams.id, vm.getUserSuccess, vm.getUserError);    
    }

    function _assignSections(id) {

        var userUpdate = new Object();
        userUpdate.Id = vm.$routeParams.id;
        userUpdate.UserId = vm.userinfo.userId;
        userUpdate.SectionId = id;
        userUpdate.EnrollmentStatusId = 2;

        vm.$userSectionsService.updateUserStatus(vm.$routeParams.id, userUpdate, vm.updateStatusSuccess, vm.updateStatusError)

        alert(vm.userinfo.firstName + " " + vm.userinfo.lastName + "has been assigned to a section");

        vm.$location.path('/users#');
        vm.$location.replace();
    }

    function _getSectionsSuccess(data) {
        vm.notify(function () {
            vm.sections = data.items;
        });
    }

    function _getUserSuccess(data) {
        vm.notify(function () {
            vm.userinfo = data.item;   
        });
    }

    function _backToList() {
        vm.$location.path('/users#');
           
        vm.$location.replace();
           
    }

    function _updateStatusSuccess(data) {
        console.log(data);
    }

    function _getSectionsError(error) {
        console.log(error);
    }

    function _getUserError(error){
        console.log(error);
    }

    function _updateStatusError(error) {
        console.log(error);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminAssignSectionsController"
, ['$scope', '$baseController', '$userSectionsService', '$routeParams','$location']
, sabio.page.adminAssignSectionsControllerFactory);