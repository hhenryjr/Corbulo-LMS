sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/modules/template/index.html',
        controller: 'modulesListController',
        controllerAs: 'modules',
        reloadOnSearch: false
    })
    .when('/add', {
        templateUrl: '/Scripts/app/admin/modules/template/manage.html',
        controller: 'modulesManagerController',
        controllerAs: 'modules',
        reloadOnSearch: false
    })
    .when('/edit/:moduleId', {
        templateUrl: '/Scripts/app/admin/modules/template/manage.html',
        controller: 'modulesManagerController',
        controllerAs: 'modules',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});