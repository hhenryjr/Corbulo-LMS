using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class TrackViewModel : BaseViewModel
    {
        public Dictionary<int, string> Formats { get; set; }
    }
}