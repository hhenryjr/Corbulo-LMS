sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/admin/wiki/templates/index.html',
        controller: 'adminWikiIndexController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })

    .when('/create', {
        templateUrl: '/Scripts/app/admin/wiki/templates/manage.html',
        controller: 'adminWikiManageController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/edit/:wikiPageId', {
        templateUrl: '/Scripts/app/admin/wiki/templates/manage.html',
        controller: 'adminWikiManageController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/edit/:wikiPageId/content', {
        templateUrl: '/Scripts/app/admin/wiki/templates/content.html',
        controller: 'adminWikiContentController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/tree', {
        templateUrl: '/Scripts/app/admin/wiki/templates/trees.html',
        controller: 'adminWikiTreeController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/space/:spaceId', {
        templateUrl: '/Scripts/app/admin/wiki/templates/tree.html',
        controller: 'adminWikiTreeController',
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