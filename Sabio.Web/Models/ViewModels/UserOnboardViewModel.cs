using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class UserOnboardViewModel : BaseViewModel
    {
        public Dictionary<int, string> Genders { get; set; }
        public Dictionary<int, string> EmploymentStatuses { get; set; }
        public Dictionary<int, string> AccreditatonId { get; set; }
        public Guid UserId { get; set; }
        public int Id { get; set; }
    }
}