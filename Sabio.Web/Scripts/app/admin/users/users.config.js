sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/users/templates/UserList.html',
        controller: 'adminUserListController',
        controllerAs: 'users',
        reloadOnSearch: false
    })
    .when('/sections/:id', {
        templateUrl: '/Scripts/app/admin/users/templates/AssignSections.html',
        controller: 'adminAssignSectionsController',
        controllerAs: 'users',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});