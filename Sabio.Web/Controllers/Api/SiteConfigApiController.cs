using Sabio.Web.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/siteconfig")]
    public class SiteConfigApiController : ApiController
    {
        private ISiteConfig _siteConfig;

        public SiteConfigApiController(ISiteConfig config)
        {
            _siteConfig = config;
        }

        [Route("config"), HttpGet]
        public HttpResponseMessage Getconfig()
        {
            return Request.CreateResponse(_siteConfig);
        }

        // Site Domain
        [Route("SiteDomain"), HttpGet]
        public string GetSiteDomain()
        {
            string key = _siteConfig.SiteDomain;
            return key;
        }

        // Base URL
        [Route("BaseUrl"), HttpGet]
        public string GetMailChimpApiKey()
        {
            string key = _siteConfig.BaseURL;
            return key;
        }

        // Google API Key
        [Route("Google"), HttpGet]
        public string GetGoogleApiKey()
        {
            string key = _siteConfig.GoogleApiKey;
            return key;
        }

        // Site Admin Email Address
        [Route("SiteAdminEmail"), HttpGet]
        public string GetSiteAdminEmailAddress()
        {
            string key = _siteConfig.SiteAdminEmailAddress;
            return key;
        }

    }
}
