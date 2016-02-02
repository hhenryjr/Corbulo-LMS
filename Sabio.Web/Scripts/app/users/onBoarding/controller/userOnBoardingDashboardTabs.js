sabio.page.userOnBoardTabsControllerFactory = function (
    $baseController
    , $scope
    , $routeParams
    , $location
    , $dashboardService

    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$scope = $scope;
    vm.$routeParams = $routeParams;
    vm.$location = $location;
    vm.$dashboardService = $dashboardService;

    vm.receiveUserProfileItems = _receiveUserProfileItems;
    vm.onReceiveUserProfileError = _onReceiveUserProfileError;
    vm.receiveCourseItems = _receiveCourseItems;
    vm.receiveCourseError = _receiveCourseError;
    vm.showFileInputButton = _showFileInputButton;
    vm.hideFileInputButton = _hideFileInputButton;
    vm.getBackGroundPicFromFile = _getBackGroundPicFromFile;
    vm.clickCoverInputFile = _clickCoverInputFile;
    vm.saveBackGroundPicture = _saveBackGroundPicture;
    vm.saveBackGroundPictureSuccess = _saveBackGroundPictureSuccess;
    vm.saveBackGroundPictureError = _saveBackGroundPictureError;
    vm.cancelBackGroundPicture = _cancelBackGroundPicture;
    vm.clickAvatarFile = _clickAvatarFile;
    vm.getAvatarPicFromFile = _getAvatarPicFromFile;
    vm.saveAvatarPicture = _saveAvatarPicture;
    vm.saveAvatarPictureSuccess = _saveAvatarPictureSuccess;
    vm.saveAvatarPictureError = _saveAvatarPictureError;
    vm.cancelAvatarPicture = _cancelAvatarPicture;
    vm.onRouteChangeSuccess = _onRouteChangeSuccess;

    vm.selectTab = _selectTab;

    vm.notify = vm.$dashboardService.getNotifier($scope);

    vm.tabs = null;
    vm.tabsUpdate = [
        { id: 1, label: 'Goals', link: '#/goals', tab: 'goals' },
        { id: 2, label: 'General Information', link: '#/generalInformation', tab: 'generalInformation' },
        { id: 3, label: 'Address', link: "#/address", tab: 'address' },
        { id: 4, label: 'About Me 1', link: '#/aboutMe1', tab: 'aboutMe1' },
        { id: 5, label: 'About Me 2', link: "#/aboutMe2", tab: 'aboutMe2' }
    ];

    vm.tabsCreate = [
        vm.tabsUpdate[0]
    ]
    vm.currentTab = null;

    _init();

    vm.$scope.$on('$routeChangeSuccess', _onRouteChangeSuccess);

    function _init() {
        settings();
        vm.$dashboardService.getUserInfoByUserId(vm.receiveUserProfileItems, vm.onReceiveUserProfileError);
    }

    function _selectTab(tab) {
        vm.currentTab = tab;
    }

    function _onRouteChangeSuccess(ev, current, prev) {

        if (current.params) {
            vm.tabs = vm.tabsUpdate;
            for (var x = 0; x < vm.tabs.length; x++) {
                vm.tabs[x].link = '#/' + vm.tabs[x].tab;

                if (current.$$route.originalPath.indexOf(vm.tabs[x].tab) > -1) {
                    _selectTab(vm.tabs[x]);
                }
            }
        }
        else {
            vm.tabs = vm.tabsCreate;
        }

    }

    function _receiveUserProfileItems(data) {


        vm.notify(function () {

            vm.user = data.item;
            if (data.item.coverPhotoPath) {

                vm.userBackgroundPic = data.item.coverPhotoPath;
                vm.cancelCoverPic = data.item.coverPhotoPath;
                vm.saveCoverButton = true;
            }
            else {
                vm.userBackgroundPic = "http://sabio.la/images/hacks/IMG_6094.JPG";
                vm.saveCoverButton = true;
            }

            if (data.item.avatarPhotoPath) {
                vm.userAvatarPic = data.item.avatarPhotoPath;
                vm.cancelAvatarPic = data.item.avatarPhotoPath;
                vm.saveAvatarButton = true;
            }
            else {
                vm.userAvatarPic = "";//"http://2.bp.blogspot.com/-CYonRunOdeg/UcYsi3yyoUI/AAAAAAAAAFk/MUPbzj3fphg/s1600/Sabio.gif";
                vm.saveAvatarButton = true;
            }
        });
    }

    function _onReceiveUserProfileError(jqXhr, error) {
        console.log(error);

    }

    function _receiveCourseItems(data) {

        vm.notify(function () {
            vm.courses = data.items;
        });
    }

    function _receiveCourseError(jqXhr, error) {
        console.log(error);
    }

    function _showFileInputButton() {
        vm.showChangePicture = true;
    }

    function _hideFileInputButton() {
        vm.showChangePicture = false;
    }

    function _clickCoverInputFile() {
        angular.element("#clickFileBtn").prev().trigger("click");
    }

    function _getBackGroundPicFromFile() {
        vm.saveCoverButton = false;
        vm.cancelMyBgPicture = false;
        var filename = angular.element("#backGroundPic")[0];
        _readCoverUrl(filename);
    }

    function _readCoverUrl(loadPicToCover) {
        if (loadPicToCover.files && loadPicToCover.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                vm.notify(function () {
                    vm.userBackgroundPic = e.target.result;
                    var bgPic = vm.userBackgroundPic;
                    angular.element("#landScapePic2").attr('src', bgPic)
                });
            }
            reader.readAsDataURL(loadPicToCover.files[0]);
        }

        vm.changeCover = true;
        vm.profileCover = false;
    }

    function _saveBackGroundPicture() {

        var formData = new FormData();
        var fileInput = angular.element("#backGroundPic")[0];
        for (i = 0; i < fileInput.files.length; i++) {

            formData.append(fileInput.files[i].name, fileInput.files[i]);
        }
        var contentType = 2;

        vm.$dashboardService.saveBackGroundPicture(formData, contentType, vm.saveBackGroundPictureSuccess, vm.saveBackGroundPictureError);

        vm.cancelMyBgPicture = true;
        vm.changeCover = false;
        vm.profileCover = true;

    }

    function _saveBackGroundPictureSuccess(data) {
        vm.$dashboardService.getUserInfoByUserId(vm.receiveUserProfileItems, vm.onReceiveUserProfileError);
        vm.coverPicture = data.items[0].path;
        vm.userBackgroundPic = vm.coverPicture;
        vm.saveCoverButton = true;
    }

    function _saveBackGroundPictureError(jqXhr, error) {
        console.log(error);
    }

    function _cancelBackGroundPicture() {

        vm.cancelMyBgPicture = true;
        vm.userBackgroundPic = vm.cancelCoverPic;
        vm.changeCover = false;
        vm.profileCover = true;

        //vm.userBackgroundPic = vm.coverPicture;
        vm.saveCoverButton = true;
    }




    function _clickAvatarFile() {

        angular.element("#triggerAvatarFile").prev().trigger("click");

    }

    function _getAvatarPicFromFile() {

        vm.saveAvatarButton = false;
        //vm.cancelMyAvatarPicture = false;
        var filename = angular.element("#avatarPic")[0];
        _readAvatarUrl(filename);
    }

    function _readAvatarUrl(loadPicToAvatar) {

        if (loadPicToAvatar.files && loadPicToAvatar.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                vm.notify(function () {
                    vm.userAvatarPic = e.target.result;
                    var avatarPic = vm.userAvatarPic;
                    angular.element("#ProfilePic2").attr('src', avatarPic)
                });
            }
            reader.readAsDataURL(loadPicToAvatar.files[0]);
        }

        vm.changeAvatar = true;
        vm.profileAvatar = false;
    }

    function _saveAvatarPicture() {

        var formData = new FormData();
        var fileInput = angular.element("#avatarPic")[0];
        for (i = 0; i < fileInput.files.length; i++) {
            formData.append(fileInput.files[i].name, fileInput.files[i]);
        }
        var contentType = 1;
        vm.$dashboardService.saveAvatarPicture(formData, contentType, vm.saveAvatarPictureSuccess, vm.saveAvatarPictureError);

        vm.changeAvatar = false;
        vm.profileAvatar = true;
        vm.cancelMyAvatarPicture = true;

    }

    function _saveAvatarPictureSuccess(data) {
        vm.$dashboardService.getUserInfoByUserId(vm.receiveUserProfileItems, vm.onReceiveUserProfileError);
        vm.thisAvatar = data.items[0].path;
        // vm.userAvatarPic = vm.thisAvatar;
        vm.saveAvatarButton = true;
    }

    function _saveAvatarPictureError(jqXhr, error) {
        console.log(error);
    }

    function _cancelAvatarPicture() {
        //vm.userAvatarPic = vm.thisAvatar;

        vm.saveAvatarButton = true;
        vm.cancelMyAvatarPicture = true;
        vm.userAvatarPic = vm.cancelAvatarPic;
        vm.changeAvatar = false;
        vm.profileAvatar = true;
        vm.$dashboardService.getUserInfoByUserId(vm.receiveUserProfileItems, vm.onReceiveUserProfileError);
    }

    function settings() {
        vm.changeAvatar = false;
        vm.profileAvatar = true;
        vm.changeCover = false;
        vm.profileCover = true;

        vm.user = null;
        vm.fileForm = null;
    }

}

sabio.ng.addController(sabio.ng.app.module
, "userOnBoardingTabsController"
, ['$baseController', '$scope', '$routeParams', '$location', '$dashboardService']
, sabio.page.userOnBoardTabsControllerFactory);


sabio.ng.app.module.directive('customOnChange', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var onChangeFunc = scope.$eval(attrs.customOnChange);
            element.bind('change', onChangeFunc);
        }
    };
});

