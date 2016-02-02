using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UserOnboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IUserOnboardService
    {
        OnboardRegistration Get(int id);

        void Update1(UserOnboardUpdate1 model);

        void Update2(UserOnboardUpdate2 model);

        void Update3(UserOnboardUpdate3 model);

        void Update4(UserOnboardUpdate4 model);

        void Update5(UserOnboardUpdate5 model);
    }
}