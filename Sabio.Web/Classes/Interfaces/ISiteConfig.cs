using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Classes
{
    public interface ISiteConfig
    {
        string BrandDescription { get;  }
        string BrandName { get; }
        string BrandTagline { get; }
        string BrandLogo { get; }
        string AwsAccessKey { get; }
        string AwsSecretAccessKey { get; }
        string BaseURL { get; }
        string GoogleApiKey { get; }
        string SiteAdminEmailAddress { get; }
        string SiteDomain { get; }
    }
}