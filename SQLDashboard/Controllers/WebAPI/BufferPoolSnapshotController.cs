using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SQLDashboard.Azure.Storage;

namespace SQLDashboard.Controllers.WebAPI
{
    //[Authorize]
    public class BufferPoolSnapshotController : ApiController
    {
        public IEnumerable<EntityWithGUID> Get(string id)
        {
            Guid guid = new Guid(id);

            var ret = SQLServersController.PerformCommand(guid, "SQLDashboard.TSQL.BufferPoolSnapshot.sql");
            
            foreach(System.Data.DataRow row in ret.Rows)
            {                                
                yield return new EntityWithGUID() { Entity = row[1].ToString(), Number = (int)row[0] };
            }                      
        }
    }
}
