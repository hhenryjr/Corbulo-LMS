using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.UserSettings
{
    public class UserSettingsRequest
    {
        public int SettingId { get; set; }

        public string SettingValue { get; set; }

    }
}