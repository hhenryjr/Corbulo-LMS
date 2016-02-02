using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IEthnicitiesService
    {
        List<Ethnicities> GetAll();
    }
}
