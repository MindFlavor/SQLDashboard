using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLDashboard.Controllers.WebAPI
{
    public class CachedPlanSnapshotController : ApiController
    {
        public IEnumerable<Models.CachedPlanSnapshotEntry> Get(string id)
        {
            Guid guid = new Guid(id);

            var ret = SQLServersController.PerformCommand(guid, "SQLDashboard.TSQL.CachedPlanSnapshot.sql");

            foreach (System.Data.DataRow row in ret.Rows)
            {
                object o = row[2];
                object j = row[3];

                yield return new Models.CachedPlanSnapshotEntry()
                {
                    cacheobjtype = row[0].ToString(),
                    objtype = row[1].ToString(),
                    count = (int)row[2],
                    sizeInBytes = (int)row[3]
                };
            }
        }
    }
}
