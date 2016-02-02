using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IUserService
    {
        IdentityUser CreateUser(string userName, string email, string password);

        bool Signin(string username, string password);

        bool ConfirmEmail(Guid tokenId);

        bool GetEmailConfirmed(string UserName);

        ApplicationUser GetUser(string username);

        ApplicationUser GetUserByEmail(string email);

        ApplicationUser GetUserbyUserName(string userName);

        ApplicationUser GetUserById(string userId);

        bool ChangePassWord(string userId, string newPassword);

        bool ResetPassword(ResetPasswordRequest model);

        bool Logout();

        IdentityUser GetCurrentUser();

        string GetCurrentUserId();

        bool IsLoggedIn();

        List<UserSettings> GetSettings();

        void UpdateSettings(UserSettingsRequest model);

        List<RolesDomain> GetRoles();

        List<NetUserDomain> GetUserList();

        void AddRole(string id, string roleId);

        void DeleteRole(string id, string roleId);
    }
}