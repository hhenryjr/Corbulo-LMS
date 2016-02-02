
sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/userinfo', {
        templateUrl: '/Scripts/app/users/dashboard/templates/tabs/userinfo.html',
        controller: 'userDashboardInfoController',
        controllerAs: 'dashboard',
        reloadOnSearch: false
    })
    .when('/sectiondetails', {
        templateUrl: '/Scripts/app/users/dashboard/templates/tabs/sectiondetails.html',
        controller: 'userSectionDetailsController',
        controllerAs: 'dashboard',
        reloadOnSearch: false
    })
   .when('/OfficeHours', {
       templateUrl: '/Scripts/app/users/dashboard/templates/tabs/OfficeHours.html',
       controller: 'userOffficeHoursController',
       controllerAs: 'dashboard',
       reloadOnSearch: false
   })
   .when('/questions/:officeHourId', {
       templateUrl: '/Scripts/app/users/questions/templates/questions.html',
       controller: 'QuestionsController',
       controllerAs: 'dashboard',
       reloadOnSearch: false
   })
    .otherwise({
        redirectTo: '/userinfo'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});