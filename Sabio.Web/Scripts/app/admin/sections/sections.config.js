sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Scripts/app/admin/sections/templates/index.html',
            controller: 'adminSectionsIndexController',
            controllerAs: 'admin',
            reloadOnSearch: false
        })
        .when('/create', {
            templateUrl: '/Scripts/app/admin/sections/templates/tabs/info.html',
            controller: 'adminSectionsManageController',
            controllerAs: 'admin',
            reloadOnSearch: false
})
    .when('/manage/:sectionId', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/info.html',
        controller: 'adminSectionsManageController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })

    .when('/manage/:sectionId/description', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/description.html',
        controller: 'adminSectionsDescriptionController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/enrollment', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/enrollment.html',
        controller: 'adminSectionsEnrollmentController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/instructors', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/instructors.html',
        controller: 'adminSectionsInstructorsController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/location', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/location.html',
        controller: 'adminSectionsLocationController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/modules', {
        templateUrl: '/Scripts/app/admin/sections/templates/tabs/modules.html',
        controller: 'adminSectionsModulesController',
        controllerAs: 'admin',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/modules/:moduleId/wiki/create', {
        templateUrl: '/Scripts/app/admin/wiki/templates/manage.html',
        controller: 'adminWikiManageController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/modules/:moduleId/wiki/edit/:wikiPageId', {
        templateUrl: '/Scripts/app/admin/wiki/templates/manage.html',
        controller: 'adminWikiManageController',
        controllerAs: 'wiki',
        reloadOnSearch: false
    })
    .when('/manage/:sectionId/modules/:moduleId/wiki/edit/:wikiPageId/content', {
        templateUrl: '/Scripts/app/admin/wiki/templates/content.html',
        controller: 'adminWikiContentController',
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