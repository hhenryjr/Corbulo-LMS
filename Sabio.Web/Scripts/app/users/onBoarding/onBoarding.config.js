sabio.ng.app.module.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when('/goals', {
        templateUrl: '/Scripts/app/users/onBoarding/templates/tabs/Goals.html',
        controller: 'userOnBoardingGoalsController',
        controllerAs: 'onBoard',
        reloadOnSearch: false
    })
    .when('/generalInformation', {
        templateUrl: '/Scripts/app/users/onBoarding/templates/tabs/GeneralInfo.html',
        controller: 'userOnBoardingGeneralInfoController',
        controllerAs: 'onBoard',
        reloadOnSearch: false
    })
    .when('/address', {
        templateUrl: '/Scripts/app/users/onBoarding/templates/tabs/Address.html',
        controller: 'userOnBoardingAddressController',
        controllerAs: 'onBoard',
        reloadOnSearch: false
    })
    .when('/aboutMe1', {
        templateUrl: '/Scripts/app/users/onBoarding/templates/tabs/AboutMe1.html',
        controller: 'userOnBoardingAboutMe1Controller',
        controllerAs: 'onBoard',
        reloadOnSearch: false
    })
    .when('/aboutMe2', {
        templateUrl: '/Scripts/app/users/onBoarding/templates/tabs/AboutMe2.html',
        controller: 'userOnBoardingAboutMe2Controller',
        controllerAs: 'onBoard',
        reloadOnSearch: false
    })

    .otherwise({
        redirectTo: '/Goals'
    });

    $locationProvider.html5Mode({
        enabled: false
    });

});