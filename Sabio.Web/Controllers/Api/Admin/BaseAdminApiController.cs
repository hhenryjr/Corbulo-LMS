using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [Authorize] //  TODO: add role checks in addition to authorize
    public class BaseAdminApiController : ApiController
    {
    }
}
