using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class EthnicitiesService : BaseService, IEthnicitiesService
    {
        public List<Ethnicities> GetAll()
        {
            List<Domain.Ethnicities> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Ethnicities_SelectAll"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   Domain.Ethnicities s = new Domain.Ethnicities();
                   int startingIndex = 0;

                   s.Id = reader.GetSafeInt32(startingIndex++);
                   s.Ethnicity = reader.GetSafeString(startingIndex++);               

                   if (list == null)
                   {
                       list = new List<Domain.Ethnicities>();
                   }

                   list.Add(s);
               }
               );


            return list;
        }

    }
}