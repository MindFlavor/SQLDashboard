using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLDashboard.Controllers.WebAPI
{
    //[Authorize]
    public class SQLServerConnectionStringsController : ApiController
    {

        // GET api/SQLServerConnectionStrings
        public IEnumerable<SQLDashboard.Azure.Storage.EntityWithGUID> Get()
        {
            var InstanceConnections = new Azure.Storage.InstanceConnections(System.Configuration.ConfigurationManager.AppSettings["Azure_Storage_Account_Connection_String"]);
            return InstanceConnections.Get();
        }

        public void Post(SQLDashboard.Azure.Storage.EntityWithGUID entity)
        {
            var InstanceConnections = new Azure.Storage.InstanceConnections(System.Configuration.ConfigurationManager.AppSettings["Azure_Storage_Account_Connection_String"]);
            InstanceConnections.Put(entity);
        }

        public void Delete(string id)
        {
            var InstanceConnections = new Azure.Storage.InstanceConnections(System.Configuration.ConfigurationManager.AppSettings["Azure_Storage_Account_Connection_String"]);
            InstanceConnections.Delete(new Guid(id));
        }
    }
}
