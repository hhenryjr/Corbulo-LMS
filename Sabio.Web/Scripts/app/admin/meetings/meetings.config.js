sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/meetings/templates/index.html',
        controller: 'adminMeetingsIndexController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/create', {
        templateUrl: '/Scripts/app/admin/meetings/templates/manage.html',
        controller: 'adminMeetingsManageController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:meetingId', {
        templateUrl: '/Scripts/app/admin/meetings/templates/manage.html',
        controller: 'adminMeetingsManageController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});