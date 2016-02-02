sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/users/templates/User.html',
        controller: 'NetUserController',
        controllerAs: 'roles',
        reloadOnSearch: false
    })
    .when('/roles/:id', {
        templateUrl: '/Scripts/app/admin/users/templates/Roles.html',
        controller: 'adminRolesController',
        controllerAs: 'roles',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});