sabio.services.utilityFactory = function ($baseService) {
    var svc = this;
    svc = $baseService.merge(true, {}, svc, $baseService);


    svc.getUserRole = _getUserRole;
    svc.getRoles = _getRoles;
    svc.userAccess = _userAccess;

    function _getUserRole(x) {
        var user = x.roles;
        svc.userRoles = [];

        for (var a = 0; a < user.length; a++) {

            var roleName = user[a].roleId;
            svc.userRoles.push({ roleId: roleName });
        }

       // console.log(svc.userRoles);
    }

    function _getRoles(roles) {
        var role = roles.items;
        svc.rolesList= [];

        for (var a = 0; a < role.length; a++) {
            var rId = role[a].id;
            var name = role[a]. name;
            svc.rolesList.push({id: rId, roleNum:a, roleName: name });
        }
       // console.log(svc.rolesList);
    }

    function _userAccess() {

        svc.access = {};
      
        var role = svc.rolesList;
        var user = svc.userRoles;
       
        for (var a = 0; a < user.length; a++) {
            for (var x = 0; x < role.length; x++) {
                if (user[a].roleId == role[x].id) {
                    //console.log(role[x].roleName);
                    switch(role[x].roleName) {
                        case "Student":
                            svc.access.students = true;
                            svc.access.admin = false
                            svc.access.instructors = false;
                            svc.access.SuperAdmin = false;
                            break;
                        case "Instructor":
                            svc.access.students = true;
                            svc.access.instructors = true;
                            svc.access.admin = false
                            svc.access.SuperAdmin = false;
                            break;
                        case "Admin":
                            svc.access.students = true;
                            svc.access.instructors = true;
                            svc.access.admins = true;
                            svc.access.SuperAdmin = false;
                            break;
                        case "SuperAdmin":
                            svc.access.students = true;
                            svc.access.instructors = true;
                            svc.access.admins = true;
                            svc.access.SuperAdmin = true;
                            break;
                        default:
                            svc.access.student = false;
                            svc.access.admin = false
                            svc.access.instructors = false;
                            svc.access.SuperAdmin = false;

                    }
                }
            }
           
        }
        //console.log(svc.access);
        return svc.access
    }

    return svc;
}

sabio.ng.addService(sabio.ng.app.module
    , "$userRoleService"
    , ["$baseService"]
    , sabio.services.utilityFactory);