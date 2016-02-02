sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: '/Scripts/app/Comments/templates/Comments.html',
        controller: 'commentsController',
        controllerAs: 'main',
        reloadOnSearch: false
    })
     //.when('/add/:wikiId', {
     //       templateUrl: '/Scripts/app/Comments/templates/Comments.html',
     //       controller: 'commentsController',
     //       controllerAs: 'comment',
     //       reloadOnSearch: false
     //   })

    .otherwise({
        redirectTo: '/'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});