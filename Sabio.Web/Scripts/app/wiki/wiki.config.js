sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/wiki/templates/index.html',
        controller: 'wikiIndexController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/space/:spaceId', {
        templateUrl: '/Scripts/app/wiki/templates/space.html',
        controller: 'wikiSpaceController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/space/:spaceId/:pageSlug', {
        templateUrl: '/Scripts/app/wiki/templates/view.html',
        controller: 'wikiPageController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});