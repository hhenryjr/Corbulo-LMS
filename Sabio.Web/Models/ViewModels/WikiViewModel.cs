using Sabio.Web.Domain.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class WikiViewModel : BaseViewModel
    {
        public List<WikiSpace> Spaces { get; set; }

        public WikiPageContentType ContentTypesEnum { get; set; }

        public Dictionary<string, string> HighlightLanguagesEnum { get; set; }

    }
}