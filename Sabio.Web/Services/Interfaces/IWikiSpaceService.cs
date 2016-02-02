using Sabio.Web.Domain.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IWikiSpaceService
    {
        List<WikiSpace> Get();
    }
}