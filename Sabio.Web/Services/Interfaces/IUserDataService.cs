using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UsersData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IUserDataService
    {
        UserInfo Get(int id);

        UserInfo GetByUserId(string UserId);

        int Insert(UserInfoAddRequest model, string userId);

        void Update(UserInfoUpdate model);

        void UpdateUserStatus(UserStatusUpdate model);

        List<UserInfo> GetsList();

        void Delete(int id);

        List<UserInfo> GetList();

        List<UserBase> GetByRole();

        bool ConfirmEmail(Guid tokenId);

        List<UserInfo> GetOnboardedStudents();
    }
}