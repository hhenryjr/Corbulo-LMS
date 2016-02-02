sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/track/templates/index.html',
        //controller: 'adminTrackIndexController',
        //controllerAs: 'track',
        reloadOnSearch: false
    })
    .when('/create', {
        templateUrl: '/Scripts/app/admin/track/templates/create.html',
        controller: 'adminTrackManageController',
        controllerAs: 'track',
        reloadOnSearch: false
    })
    .when('/edit/:trackId', {
        templateUrl: '/Scripts/app/admin/track/templates/manage.html',
        controller: 'adminTrackManageController',
        controllerAs: 'track',
        reloadOnSearch: false
    })
    .when('/edit/course/:courseId', {
        templateUrl: '/Scripts/app/admin/track/templates/course.html',
        controller: 'adminTrackContentController',
        controllerAs: 'track',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});