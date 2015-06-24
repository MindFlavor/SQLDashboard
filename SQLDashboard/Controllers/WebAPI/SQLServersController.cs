using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using SQLDashboard.Azure.Storage;

namespace SQLDashboard.Controllers.WebAPI
{
    //[Authorize]
    public class SQLServersController : ApiController
    {
        protected static SQLDashboard.Azure.Storage.SQLConnectionString[] _SQLServers = new SQLConnectionString[0];
        static DateTime dtLastUpdate = DateTime.MinValue;
        const int SECONDS_CACHE = 10;

        static SQLDashboard.Azure.Storage.InstanceConnections InstanceConnections = null;

        protected SQLDashboard.Azure.Storage.SQLConnectionString[] SQLServers
        {
            get
            {
                lock (_SQLServers)
                {
                    if ((_SQLServers.Length == 0) || ((DateTime.Now - dtLastUpdate).TotalSeconds > SECONDS_CACHE))
                    {
                        if (InstanceConnections == null)
                        {
                            InstanceConnections = new Azure.Storage.InstanceConnections(System.Configuration.ConfigurationManager.AppSettings["Azure_Storage_Account_Connection_String"]);
                        }

                        var ret = InstanceConnections.Get();

                        dtLastUpdate = DateTime.Now;
                        _SQLServers = new SQLDashboard.Azure.Storage.SQLConnectionString[ret.Count()];
                        {
                            int i = 0;
                            foreach (var entry in ret)
                            {
                                System.Data.SqlClient.SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(entry.Entity);

                                _SQLServers[i++] = new SQLConnectionString()
                                {
                                    GUID = entry.GUID,
                                    ApplicationIntent = csb.ApplicationIntent,
                                    ConnectTimeout = csb.ConnectTimeout,
                                    ApplicationName = csb.ApplicationName,
                                    DataSource = csb.DataSource,
                                    Encrypt = csb.Encrypt,
                                    InitialCatalog = csb.InitialCatalog,
                                    IntegratedSecurity = csb.IntegratedSecurity,
                                    Password = csb.Password,
                                    PersistSecurityInfo = csb.PersistSecurityInfo,
                                    TrustServerCertificate = csb.TrustServerCertificate,
                                    UserID = csb.UserID,
                                    WorkstationID = csb.WorkstationID
                                };
                            }
                        }
                        //_SQLServers = new SQLDashboard.Azure.Storage.SQLConnectionString[]
                        //{
                        //    new SQLDashboard.Azure.Storage.SQLConnectionString() { DataSource="FRCOGNO81.europe.corp.microsoft.com\\V12", IntegratedSecurity = true, ConnectTimeout=5, GUID = Guid.Parse("84b84c25-a2ac-43de-a595-19ddfbc84fbc")  },
                        //    new SQLDashboard.Azure.Storage.SQLConnectionString() { DataSource="FRCOGNO81.europe.corp.microsoft.com\\V14", IntegratedSecurity = true, ConnectTimeout=5, GUID = Guid.Parse("AAb84c25-a2ac-43de-a595-19ddfbc84fbc") },
                        //};
                    }
                }
                return _SQLServers;
            }
        }


        // GET api/SQLServers
        public IEnumerable<SQLDashboard.Azure.Storage.SQLConnectionString> Get()
        {
            return SQLServers;
        }

        // GET api/SQLServers/{guid}
        public EntityWithGUID Get(string id)
        {
            try
            {
                Guid gID = Guid.Parse(id);
                var dt = PerformCommand(gID, "SQLDashboard.TSQL.InstanceSummary.sql");

                var ret = new EntityWithGUID() { Number = (int)dt.Rows[0][0], Entity = null };
                return ret;
            }
            catch (Exception exce)
            {
                return new EntityWithGUID() { Number = -1, Entity = exce.Message };
            }
        }

        #region Support methods
        internal static System.Data.DataTable PerformCommand(Guid gDB, string ScriptName)
        {
            string tsql;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(ScriptName)))
            {
                tsql = sr.ReadToEnd();
            }

            SQLConnectionString sqlServer;
            lock (_SQLServers)
            {
                sqlServer = _SQLServers.First(item => item.GUID.Equals(gDB));
            }

            System.Data.DataTable dt = new System.Data.DataTable();

            using (SqlConnection conn = new SqlConnection(sqlServer.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(tsql, conn))
                {
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        ada.Fill(dt);
                    }
                }
            }

            return dt;

        }
        #endregion
    }
}
