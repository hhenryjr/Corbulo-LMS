using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class WikiSpaceService : BaseService, IWikiSpaceService
    {
        public List<WikiSpace> Get()
        {
            List<WikiSpace> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiSpaces_SelectAll"
                , inputParamMapper: null, map: delegate(IDataReader reader, short set)
                {
                    WikiSpace ws = new WikiSpace();
                    int startingIndex = 0;

                    ws.Id = reader.GetSafeInt32(startingIndex++);
                    ws.Title = reader.GetSafeString(startingIndex++);
                    ws.ParentId = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<WikiSpace>();
                    }

                    list.Add(ws);
                }
            );
            return list;
        }
    }
}