
sabio.page.rolesControllerFactory = function ($scope, $baseController, $rolesService, $routeParams) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;

    vm.$routeParams = $routeParams;

    vm.$rolesService = $rolesService;

    vm.rolesList = {};
    vm.chosen = { Roles: [] };

    vm.getRolesSuccess = _getrolesSuccess;
    vm.getRolesError = _getRolesError;
    vm.getUserRolesSuccess = _getUserRolesSuccess;
    vm.getUserRolesError = _getUserRolesError;

    vm.CheckBoxClicked = _CheckBoxClicked;
    vm.onRoleAddedSuccess = _onRoleAddedSuccess;
    vm.onRoleAddError = _onRoleAddError;
    vm.onDeleteSuccess = _onDeleteSuccess;
    vm.onDeleteError = _onDeleteError;

    vm.notify = vm.$rolesService.getNotifier($scope);

    render();

    function render() {
        vm.$rolesService.getRoles(vm.getRolesSuccess, vm.getRolesError);
        vm.$rolesService.getUserRoleById(vm.$routeParams.id, vm.getUserRolesSuccess, vm.getUserRolesError);
    }

    function _getrolesSuccess(data) {
        vm.notify(function () {
            vm.assign = data.items;
            vm.rolesList.admin = vm.assign[0].id;
            vm.rolesList.instructor = vm.assign[1].id;
            vm.rolesList.student = vm.assign[2].id;
            vm.rolesList.superadmin = vm.assign[3].id;
        });
    }
    function _getRolesError(error) {
        console.log(error);
    }

    function _getUserRolesSuccess(data) {

        vm.notify(function () {
            vm.aspNetUser = data.item;
            for (x = 0; x < vm.aspNetUser.roles.length; x++)
            vm.chosen.Roles.push(vm.aspNetUser.roles[x].roleId)
        });
    }

    function _getUserRolesError(error) {
        console.log(error);
    }

    function _CheckBoxClicked(id, checked) {
        if (vm.$routeParams.id) {
            if (checked === true) {
                vm.$rolesService.addRoles(vm.$routeParams.id, id, vm.onRoleAddedSuccess, vm.onRoleAddError);
            }
            else if (checked === false) {
                vm.$rolesService.deleteRole(vm.$routeParams.id, id, vm.onDeleteSuccess, vm.onDeleteError);
            }
        }
        else {
            console.log("Need to redirect to user Info page");
        }
            
    }

    function _onRoleAddedSuccess(data) {
        console.log(data);
    }

    function _onRoleAddError(error) {
        console.log(error);
    }

    function _onDeleteSuccess(data) {
        console.log(data);
    }

    function _onDeleteError(error) {
        console.log(error);
    }

}

sabio.ng.addController(sabio.ng.app.module
, "adminRolesController"
, ['$scope', '$baseController', '$rolesService', '$routeParams']
, sabio.page.rolesControllerFactory);